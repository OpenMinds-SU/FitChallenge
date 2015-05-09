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
    public class CalendarController : Controller
    {
        private IFitChallengeDbContext context;

        public CalendarController(IFitChallengeDbContext context)
        {
            this.context = context;
        }

        public ActionResult Index(DateTime? startDate = null, DateTime? endDate = null)
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
                var events = this.context.Events
                    .Where(ev => ev.UserId == this.User.Identity.GetUserId())
                    .Where(ev => ev.Date >= startDate && ev.Date <= endDate)
                    .Select(ev => new EventViewModel
                    {
                        Id = ev.Id,
                        Date = ev.Date,
                        Food = ev.Food,
                        Supplements = ev.Supplements,
                        IsTrainingDone = ev.IsTrainingDone,
                        SupplementsAreDrunken = ev.SupplementsAreDrunken,
                        UserId = ev.UserId,
                        WorkoutId = ev.WorkoutId
                    })
                    .ToArray();

                return this.Json(new { status = "success", data = events }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return this.Json(new { status = "error" });
            }
        }

        public ActionResult Create()
        {
            var model = new EventViewModel();

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Create(EventViewModel eventModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var eventDataModel = new Event
                    {
                        Date = eventModel.Date,
                        Food = eventModel.Food,
                        Supplements = eventModel.Supplements,
                        IsTrainingDone = eventModel.IsTrainingDone,
                        SupplementsAreDrunken = eventModel.SupplementsAreDrunken,
                        UserId = eventModel.UserId,
                        WorkoutId = eventModel.WorkoutId
                    };

                    this.context.Events.Add(eventDataModel);
                    this.context.SaveChanges();

                    return Json(new { status = "success", message = "Event was successfully created!", data = eventDataModel });
                }
                catch (Exception)
                {
                    return Json(new { status = "error" });
                }
            }
            var allModelStateErrors = ModelState.Values.Select(values => values.Errors.Select(error => error.ErrorMessage));

            return Json(new { status = "error", errors = allModelStateErrors });
        }

        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return Json(new { status = "error", message = "Id is required!" });
            }

            try
            {
                var currentEvent = this.context.Events.Where(ev => ev.Id == id)
                    .Select(ev => new EventViewModel
                    {
                        Id = ev.Id,
                        Date = ev.Date,
                        Food = ev.Food,
                        Supplements = ev.Supplements,
                        IsTrainingDone = ev.IsTrainingDone,
                        SupplementsAreDrunken = ev.SupplementsAreDrunken,
                        UserId = ev.UserId,
                        WorkoutId = ev.WorkoutId
                    })
                    .FirstOrDefault();

                return this.Json(new { status = "success", data = currentEvent }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return this.Json(new { status = "error" });
            }
        }

        [HttpPost]
        public ActionResult Edit(EventViewModel eventModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var eventDataModel = new Event
                    {
                        Date = eventModel.Date,
                        Food = eventModel.Food,
                        Supplements = eventModel.Supplements,
                        IsTrainingDone = eventModel.IsTrainingDone,
                        SupplementsAreDrunken = eventModel.SupplementsAreDrunken,
                        UserId = eventModel.UserId,
                        WorkoutId = eventModel.WorkoutId
                    };

                    var message = string.Empty;

                    if (eventModel.Id != 0)
                    {
                        var currentEvent = this.context.Events.Where(ev => ev.Id == eventModel.Id)
                            .FirstOrDefault();

                        currentEvent.Date = eventModel.Date;
                        currentEvent.Food = eventModel.Food;
                        currentEvent.Supplements = eventModel.Supplements;
                        currentEvent.IsTrainingDone = eventModel.IsTrainingDone;
                        currentEvent.SupplementsAreDrunken = eventModel.SupplementsAreDrunken;
                        currentEvent.UserId = eventModel.UserId;
                        currentEvent.WorkoutId = eventModel.WorkoutId;

                        this.context.SaveChanges();
                        message = "Event was successfully updated!";
                    }
                    else
                    {
                        this.context.Events.Add(eventDataModel);
                        this.context.SaveChanges();
                        message = "Event was successfully created!";
                    }

                    return Json(new { status = "success", message = message, data = eventDataModel });
                }
                catch (Exception)
                {
                    return Json(new { status = "error" });
                }
            }
            var allModelStateErrors = ModelState.Values.Select(values => values.Errors.Select(error => error.ErrorMessage));

            return Json(new { status = "error", errors = allModelStateErrors });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return Json(new { status = "error", message = "Id is required!" });
            }

            try
            {
                var entityToRemove = this.context.Events.Where(ev => ev.Id == id)
                            .FirstOrDefault();

                this.context.Events.Remove(entityToRemove);

                return Json(new { status = "success", message = "Event was successfully removed", data = entityToRemove });
            }
            catch (Exception)
            {
                return Json(new { status = "error" });
            }
        }
    }
}