using Fmi.OpenMinds.FitChallenge.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Fmi.OpenMinds.FitChallenge.Models;

namespace Fmi.OpenMinds.FitChallenge.Web.Controllers
{
    [Authorize]
    public class MeasurementController : Controller
    {
        private IFitChallengeDbContext context;

        public MeasurementController(IFitChallengeDbContext context)
        {
            this.context = context;
        }

        public ActionResult Index(string userId = null)
        {
            if (userId == null)
            {
                userId = User.Identity.GetUserId();
            }
            else
            {
                // Check if current user is trainer to selected user.
                string currentUserId = User.Identity.GetUserId();

                bool hasApprovedRequest = this.context.TrainingScheduleRequests
                    .Any(r => r.SportsmanId == userId &&
                        r.InstructorId == currentUserId &&
                        r.State == TrainingScheduleRequestState.Approved);

                if (!hasApprovedRequest)
                {
                    return this.View("Error");
                }
            }

            var userMeasurements = this.context.Measurements
                .Where(m => m.UserId == userId)
                .OrderBy(m => m.Date);

            return View(userMeasurements);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var measurement = new Measurement() 
            {
                Date = DateTime.Today
            };
            var currentUserId = User.Identity.GetUserId();
            var lastMeasurement = this.context.Measurements
                .Where(m => m.UserId == currentUserId)
                .OrderByDescending(m => m.Date)
                .FirstOrDefault();

            if(lastMeasurement != null)
            {
                measurement.MeasurementValues = lastMeasurement.MeasurementValues;
            }

            return View(measurement);
        }

        [HttpPost]
        public ActionResult Create(Measurement measurementView)
        {
            if(!ModelState.IsValid)
            {
                return View(measurementView);
            }

            string userId = User.Identity.GetUserId();
            var measurement = new Measurement() 
            {
                Date = measurementView.Date,
                UserId = userId
            };
                
            context.Measurements.Add(measurement);

            MeasurementType measurementTypeIndex = MeasurementType.Weight;
            foreach (var measurementValue in measurementView.MeasurementValues)
            {
                measurementValue.Measurement = measurement;
                measurementValue.MeasurementType = measurementTypeIndex;
                measurement.MeasurementValues.Add(measurementValue);
                measurementTypeIndex++;
            }

            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var measurementEdit = context.Measurements.Find(id);
            return View(measurementEdit);
        }

        [HttpPost]
        public ActionResult Edit(Measurement measurementEdit)
        {
            if (!ModelState.IsValid)
            {
                return View(measurementEdit);
            }

            var measurementNew = context.Measurements.Find(measurementEdit.Id);
            int indexMeasurementValue = 0;
            var measurementNewValuesArray = measurementNew.MeasurementValues.ToList();

            foreach (var measurementValue in measurementEdit.MeasurementValues)
            {
                measurementNewValuesArray[indexMeasurementValue].Value = measurementValue.Value; 
                indexMeasurementValue++;
            }
            measurementNew.MeasurementValues = measurementNewValuesArray;
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            return View(id);
        }

        [HttpPost]
        public ActionResult Delete(Measurement measurementDelete)
        {
            try
            {
                var measurementToDelete = context.Measurements.Find(measurementDelete.Id);
                context.Measurements.Remove(measurementToDelete);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult TraineesList()
        {
            string userId = User.Identity.GetUserId();
            var traineesIds = this.context.TrainingScheduleRequests
                .Where(r => r.InstructorId == userId &&
                    r.State == TrainingScheduleRequestState.Approved)
                .Select(r => r.SportsmanId);
            var trainees = this.context.Users.Where(u => traineesIds.Contains(u.Id));

            return View(trainees);
        }
    }
}
