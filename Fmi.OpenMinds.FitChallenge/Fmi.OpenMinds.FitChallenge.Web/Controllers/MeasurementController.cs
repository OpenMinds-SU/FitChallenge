using Fmi.OpenMinds.FitChallenge.Data;
using System;
using System.Collections.Generic;
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

        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            var userMeasurements = this.context.Measurements
                .Where(w => w.UserId == currentUserId || w.UserId == null);

            return View(userMeasurements);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var measurement = new Measurement();
            return View(measurement);
        }

        [HttpPost]
        public ActionResult Create(Measurement measurementView)
        {
            try
            {
                var measurement = new Measurement();
                measurement.UserId = User.Identity.GetUserId();

                MeasurementType measurementTypeIndex = MeasurementType.Weight;

                foreach (var measurementValue in measurementView.MeasurementValues)
                {
                    measurementValue.Measurement = measurement;
                    measurementValue.MeasurementType = measurementTypeIndex;
                    context.MeasurementValues.Add(measurementValue);
                    measurementTypeIndex++;
                }

                context.Measurements.Add(measurement);
                context.SaveChanges();
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View(measurementView);
            }
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
