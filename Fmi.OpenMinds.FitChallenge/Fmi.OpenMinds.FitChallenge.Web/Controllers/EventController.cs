namespace Fmi.OpenMinds.FitChallenge.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Fmi.OpenMinds.FitChallenge.Data;
    using Fmi.OpenMinds.FitChallenge.Models;

    public class EventController : Controller
    {
        private IFitChallengeDbContext context;

        public EventController(IFitChallengeDbContext context)
        {
            this.context = context;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}