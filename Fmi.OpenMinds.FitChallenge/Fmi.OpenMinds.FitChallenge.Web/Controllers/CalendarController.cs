namespace Fmi.OpenMinds.FitChallenge.Web.Controllers
{
    using System.Web.Mvc;
    using Fmi.OpenMinds.FitChallenge.Data;

    [Authorize]
    public class CalendarController : Controller
    {
        private IFitChallengeDbContext context;

        public CalendarController(IFitChallengeDbContext context)
        {
            this.context = context;
        }

        public ActionResult Index()
        {
            return this.View();
        }
    }
}