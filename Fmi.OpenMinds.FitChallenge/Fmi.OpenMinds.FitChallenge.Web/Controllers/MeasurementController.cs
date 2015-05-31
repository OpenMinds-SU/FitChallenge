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
    public class MeasurementController : Controller
    {
        private IFitChallengeDbContext context;

        public MeasurementController(IFitChallengeDbContext context)
        {
            this.context = context;
        }

        public ActionResult Index(string userId = null)
        {
            // TODO: check if currentUser can view measurements of user with specified userId;
            
            if (userId == null)
            {
                userId = User.Identity.GetUserId();
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

            var lastMeasurement = this.context.Measurements
                .OrderByDescending(x => x.Date)
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
        public ActionResult Edit(Measurement measurementEdit, List<Double> measurements)
        {
            try
            { 
                //foreach (Employee Emp in employees)
                //{
                //    Employee Existed_Emp = DbCompany.Employees.Find(Emp.ID);
                //    Existed_Emp.Name = Emp.Name;
                //    Existed_Emp.Gender = Emp.Gender;
                //    Existed_Emp.Company = Emp.Company;
                //}
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
    }
}
