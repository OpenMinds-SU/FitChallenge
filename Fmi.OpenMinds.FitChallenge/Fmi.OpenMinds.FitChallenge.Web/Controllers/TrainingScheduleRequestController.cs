using Fmi.OpenMinds.FitChallenge.Data;
using Fmi.OpenMinds.FitChallenge.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fmi.OpenMinds.FitChallenge.Web.Controllers
{
    [Authorize]
    public class TrainingScheduleRequestController : Controller
    {
        private IFitChallengeDbContext context;
        private ApplicationUserManager userManager;

        public TrainingScheduleRequestController(IFitChallengeDbContext context) 
        {
            this.context = context;
        }

        public TrainingScheduleRequestController(IFitChallengeDbContext context, ApplicationUserManager userManager) 
        {
            this.context = context;
            this.userManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                userManager = value;
            }
        }

        // Filtering The TrainingScheduleRequests for The Current User
        [NonAction]
        private IQueryable<TrainingScheduleRequest> GetCurrentUserTrainingScheduleRequests(int trainingScheduleStateBitmask)
        {
            var currentUserId = User.Identity.GetUserId();
            var userTrainingScheduleRequests = context.TrainingScheduleRequests
                .Where(req => req.SportsmanId != null && req.InstructorId != null
                                                      && (req.SportsmanId == currentUserId || req.InstructorId == currentUserId)
                                                      && (((int)req.State & trainingScheduleStateBitmask) != 0));

            return userTrainingScheduleRequests;
        }

        // Converting training schedule state string to TrainingScheduleRequest specific list view name
        [NonAction]
        private string GetTrainingScheduleRequestViewName(TrainingScheduleRequestState trainSchedState)
        {
            string resListViewName = "";

            switch (trainSchedState)
            {
                case TrainingScheduleRequestState.New:
                    resListViewName = "ListRequested";
                    break;
                case TrainingScheduleRequestState.Approved:
                    resListViewName = "ListActive";
                    break;
                default:
                    resListViewName = "Index";
                    break;
            }

            return resListViewName;
        }

        // GET: TrainingScheduleRequest
        [HttpGet]
        public ActionResult Index()
        {
            return View(GetCurrentUserTrainingScheduleRequests((int)~TrainingScheduleRequestState.Unknown)); // bit mask that match all valid states
        }

        // GET: TrainingScheduleRequest/List
        [HttpGet]
        public ActionResult List(TrainingScheduleRequestState? nState)
        {
            var currentUserId = User.Identity.GetUserId();
            var user = context.Users.Find(currentUserId);
            if ((null != nState) && (null != user) && (true == UserManager.IsInRole(user.Id, "Instructor")))
            {
                TrainingScheduleRequestState state = nState.Value;
                string viewName = GetTrainingScheduleRequestViewName(state);
                var requests = GetCurrentUserTrainingScheduleRequests((int)state);
                return View(viewName, requests);
            }
            else
            {
                // Current user is a sportsman and should not see the List<Specific> view but the Index (i.e. ListAll) view.
                return RedirectToAction("Index");
            }
        }

        // GET : TrainingScheduleRequest/Create
        [HttpGet]
        public ActionResult Create(string reqInstructorId)
        {
            // Verify if the provided instructor Id matches with an Id of a User which has an Istructor role
            var user = context.Users.Find(reqInstructorId);
            if ((null == user) || ((null != user) && (false == UserManager.IsInRole(user.Id, "Instructor"))))
            {
                ViewBag.Error = "Invalid instructor Id for the Create request.";
            }

            if (null != ViewBag.Error)
            {
                return View(GetCurrentUserTrainingScheduleRequests((int)~TrainingScheduleRequestState.Unknown)); // bit mask that match all states
            }

            var currentUserId = User.Identity.GetUserId();
            var trainingScheduleRequestModel = new TrainingScheduleRequest {
                                                        SportsmanId = currentUserId,
                                                        Sportsman = context.Users.Find(currentUserId),
                                                        InstructorId = reqInstructorId,
                                                        Instructor = context.Users.Find(reqInstructorId)};
            
            return View("Edit", trainingScheduleRequestModel);
        }

        // POST : TrainingScheduleRequest/Create
        [HttpPost]
        [ValidateInput(false)] // annotation to get rid of the annoying "A potentially dangerous Request.Form value was detected from the client" error. HTML Encoding is performed by default by the Razor.
        public ActionResult Create(TrainingScheduleRequest req)
        {
            if (req.InstructorId == null)
            {
                ModelState.AddModelError("InstructorId", "An instructor should be selected for the request.");
            }

            if (req.SportExperienceYearsRange == SportExperienceYearsRange.Unknown)
            {
                ModelState.AddModelError("SportExperienceYearsRange", "Please select your sport experience.");
            }

            if (!ModelState.IsValid)
            {
                return View("Edit", req);
            }
            System.Diagnostics.Debug.WriteLine("Adding req=\"{0}\".\n ---------------\n", req);
            var trainingScheduleRequest = new TrainingScheduleRequest {
                                                        SportsmanId = req.SportsmanId,
                                                        Sportsman = context.Users.Find(req.SportsmanId),
                                                        InstructorId = req.InstructorId,
                                                        Instructor = context.Users.Find(req.InstructorId),
                                                        SportExperienceYearsRange = req.SportExperienceYearsRange,
                                                        Message = req.Message};
            trainingScheduleRequest.Sportsman.Height = req.Sportsman.Height;
            trainingScheduleRequest.Sportsman.Weight = req.Sportsman.Weight;
            trainingScheduleRequest.Sportsman.Age = req.Sportsman.Age;
            trainingScheduleRequest.State = TrainingScheduleRequestState.New;
            trainingScheduleRequest.CreationDate = DateTime.Now;
            System.Diagnostics.Debug.WriteLine("Adding req=\"{0}\".", trainingScheduleRequest);
            context.TrainingScheduleRequests.Add(trainingScheduleRequest);
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return RedirectToAction("Index");
        }

        // GET : TrainingScheduleRequest/ChangeState
        [HttpGet]
        public ActionResult ChangeState(int reqId, TrainingScheduleRequestState? nNewState)
        {
            var currentUserId = User.Identity.GetUserId();
            var user = context.Users.Find(currentUserId);
            if ((null != nNewState) && (null != user) && (true == UserManager.IsInRole(user.Id, "Instructor")))
            {
                TrainingScheduleRequestState newState = nNewState.Value;
                TrainingScheduleRequestState probablePrevState = TrainingScheduleRequestStateMachine.getFirstDirectPrevState(newState);
                var req = context.TrainingScheduleRequests.Find(reqId);
                if (null != req)
                {
                    if (req.InstructorId == user.Id)
                    {
                        if (true == TrainingScheduleRequestStateMachine.isValidTransition(req.State, newState))
                        {
                            req.State = newState;
                            context.SaveChanges();
                        }
                        else
                        {
                            ViewBag.Error = String.Format("Invalid transition of Training Schedule Request states from {0} to {1}.", req.State, newState);
                        }
                    }
                    else
                    {
                        ViewBag.Error = String.Format("Instructor with Id={0} is not allowed to modify request with id={1}.", user.Id, req.Id);
                    }
                }
                else
                {
                    ViewBag.Error = String.Format("Request with Id={0} is not found.", reqId);
                }

                if (null != ViewBag.Error)
                {
                    // return to the same view but with provided error message
                    string viewName = GetTrainingScheduleRequestViewName(probablePrevState);
                    var requests = GetCurrentUserTrainingScheduleRequests((int)probablePrevState);
                    return View(viewName, requests);
                }
                else
                {
                    TrainingScheduleRequestState redirectState = (false == TrainingScheduleRequestStateMachine.isFinalState(newState)) ? newState : probablePrevState;
                    return RedirectToAction("List", new { nState = redirectState });
                }
            }
            else
            {
                // Current user is a sportsman and should be redirected to the Index (i.e. ListAll) view.
                return RedirectToAction("Index");
            }
        }
    }
}