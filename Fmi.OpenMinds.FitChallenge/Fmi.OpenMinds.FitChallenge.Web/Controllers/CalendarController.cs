namespace Fmi.OpenMinds.FitChallenge.Web.Controllers
{
    using System.Web.Mvc;
    using Fmi.OpenMinds.FitChallenge.Data;
    using System;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Fmi.OpenMinds.FitChallenge.Web.Models;
    using Fmi.OpenMinds.FitChallenge.Models;

    [Authorize]
    public class CalendarController : BaseController
    {
        private readonly IFitChallengeDbContext context;

        public CalendarController(IFitChallengeDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult Index(string userId = null)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                ViewBag.UserId = userId;
            }

            return this.View();
        }

        [HttpPost]
        public ActionResult Index(DateTime? startDate = null, DateTime? endDate = null, string userId = null)
        {
            if (startDate == null)
            {
                //first day of the month
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
            if (endDate == null)
            {
                //last day of the month
                endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            }

            try
            {
                var currentUserId = this.User.Identity.GetUserId();
                if (!string.IsNullOrEmpty(userId))
                {
                    currentUserId = userId;
                }

                var events = this.context.Events
                    .Where(ev => ev.UserId == currentUserId &&
                        ev.Date >= startDate && ev.Date <= endDate)
                    .ToArray()
                    .Select(ev => mapEvent(ev));

                return this.Json(new { success = true, data = events }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return this.Json(new { success = false });
            }
        }

        public ActionResult Create(string userId = null)
        {
            InitWorkoutSelectList();

            var model = new EventViewModel();
            if (string.IsNullOrEmpty(userId))
            {
                model.UserId = userId;
            }

            return this.PartialView("Edit", model);
        }

        public ActionResult Edit(int id, string userId =null)
        {
            InitWorkoutSelectList();

            var currentEvent = this.context.Events.Find(id);
            var eventViewModel = mapEvent(currentEvent);
            if (!string.IsNullOrEmpty(userId))
            {
                eventViewModel.UserId = userId;
            }

            return this.PartialView(eventViewModel);
        }

        private void InitWorkoutSelectList()
        {
            var userId = this.User.Identity.GetUserId();

            ViewBag.WorkoutSelectList = this.context.Workouts
                .Where(item => item.UserId == userId)
                .Select(w => new SelectListItem() { Value = w.Id.ToString(), Text = w.Name });
        }

        [HttpPost]
        public ActionResult Save(EventViewModel eventModel)
        {
            if (!ModelState.IsValid)
            {
                var allModelStateErrors = ModelState.Values
                    .Select(values => values.Errors.Select(error => error.ErrorMessage));
                return Json(new { success = false, errors = allModelStateErrors });
            }

            try
            {
                string message = string.Empty;
                var currentUserId = this.User.Identity.GetUserId();

                if (eventModel.Id > 0)
                {
                    var currentEvent = this.context.Events.Find(eventModel.Id);
                    if (!string.IsNullOrEmpty(eventModel.UserId))
                    {
                        currentUserId = eventModel.UserId;
                    }

                    if (currentEvent.UserId == currentUserId)
                    {
                        currentEvent.Food = eventModel.Food;
                        currentEvent.Supplements = eventModel.Supplements;
                        currentEvent.IsTrainingDone = eventModel.IsTrainingDone;
                        currentEvent.SupplementsAreDrunken = eventModel.SupplementsAreDrunken;
                        currentEvent.WorkoutId = eventModel.WorkoutId;
                        currentEvent.Workout = context.Workouts.Find(currentEvent.WorkoutId);
                        
                        this.context.SaveChanges();
                        var eventViewModel = mapEvent(currentEvent);
                        return Json(new { success = true, message = "Event was successfully updated!", data = eventViewModel });
                    }
                    else
                    {
                        return Json(new { success = false, message = "The event you are trying to modify is not yours" });
                    }
                }
                else
                {
                    var eventDataModel = new Event
                    {
                        Date = eventModel.Date,
                        Food = eventModel.Food,
                        Supplements = eventModel.Supplements,
                        IsTrainingDone = eventModel.IsTrainingDone,
                        SupplementsAreDrunken = eventModel.SupplementsAreDrunken,
                        WorkoutId = eventModel.WorkoutId
                    };

                    eventDataModel.UserId = currentUserId;
                    if (!string.IsNullOrEmpty(eventModel.UserId))
                    {
                        var isCurrentUserTrainer = context.TrainingScheduleRequests.Any(request => request.InstructorId == currentUserId
                            && request.SportsmanId == eventModel.UserId
                            && request.State == TrainingScheduleRequestState.Approved);

                        if (isCurrentUserTrainer)
                        {
                            eventDataModel.UserId = eventModel.UserId;
                        }
                    }

                    this.context.Events.Add(eventDataModel);
                    this.context.SaveChanges();

                    if (eventDataModel.Workout == null)
                    {
                        eventDataModel.Workout = context.Workouts.Find(eventDataModel.WorkoutId);
                    }

                    var eventViewModel = mapEvent(eventDataModel);
                    return Json(new { success = true, message = "Event was successfully created!", data = eventViewModel });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return Json(new { success = false, message = "Id is required!" });
            }

            try
            {
                var entityToRemove = this.context.Events.Find(id);
                if (entityToRemove == null)
                {
                    return Json(new { success = false, message = "Event does not exists." });
                }

                if (entityToRemove.UserId != User.Identity.GetUserId()) 
                {
                    return Json(new { success = false, message = "The event you are trying to delete is not yours." });
                }

                this.context.Events.Remove(entityToRemove);
                this.context.SaveChanges();

                return Json(new { success = true, message = "Event was successfully removed", data = entityToRemove });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        private EventViewModel mapEvent(Event ev)
        {
            return new EventViewModel
            {
                Id = ev.Id,
                Date = ev.Date,
                Food = ev.Food,
                Supplements = ev.Supplements,
                IsTrainingDone = ev.IsTrainingDone,
                SupplementsAreDrunken = ev.SupplementsAreDrunken,
                WorkoutId = ev.WorkoutId,
                WorkoutName = ev.Workout.Name
            };
        }
    }
}