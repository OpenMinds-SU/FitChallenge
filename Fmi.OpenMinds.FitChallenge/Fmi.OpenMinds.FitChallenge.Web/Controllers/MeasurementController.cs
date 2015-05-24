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
        public ActionResult Create(Measurement measurement)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View(measurement);
            }
        }

        public ActionResult Edit(int id)
        {
            var measurementEdit = context.Measurements.Find(id);
            return View(measurementEdit);
        }

        [HttpPost]
        public ActionResult Edit(Measurement measurementEdit)
        {
            try
            {
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
