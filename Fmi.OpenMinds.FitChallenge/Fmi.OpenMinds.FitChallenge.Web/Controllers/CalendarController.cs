namespace Fmi.OpenMinds.FitChallenge.Web.Controllers
{
    using System.Web.Mvc;
    using Fmi.OpenMinds.FitChallenge.Data;
    using System;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Fmi.OpenMinds.FitChallenge.Web.Models;

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

        public ActionResult Create(EventViewModel eventModel)
        {
            if (ModelState.IsValid)
            {
                
            }


            return this.View();
        }

        public ActionResult Edit(int id)
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Edit(EventViewModel id)
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return this.View();
        }
    }
}