﻿using Fmi.OpenMinds.FitChallenge.Data;
using Fmi.OpenMinds.FitChallenge.Models;
using Microsoft.AspNet.Identity;
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

        public TrainingScheduleRequestController(IFitChallengeDbContext context) 
        {
            this.context = context;
        }

        // Filtering The TrainingScheduleRequests for The Current User
        [NonAction]
        public IQueryable<TrainingScheduleRequest> GetCurrentUserTrainingScheduleRequests()
        {
            var currentUserId = User.Identity.GetUserId();
            var userTrainingScheduleRequests = context.TrainingScheduleRequests
                .Where(req => req.SportsmanId != null && req.InstructorId != null && (req.SportsmanId == currentUserId || req.InstructorId == currentUserId));

            return userTrainingScheduleRequests;
        }

        // GET: TrainingScheduleRequest
        public ActionResult Index()
        {
            return View(GetCurrentUserTrainingScheduleRequests());
        }

        // GET : TrainingScheduleRequest/Create
        [HttpGet]
        public ActionResult Create(string reqInstructorId)
        {
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
            trainingScheduleRequest.State = TrainingScheduleRequestState.Approved; // TODO - Change this state to be initially TrainingScheduleRequestState.New
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
    }
}