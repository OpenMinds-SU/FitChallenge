using System.Web.Mvc;
using Fmi.OpenMinds.FitChallenge.Data;

namespace Fmi.OpenMinds.FitChallenge.Web.Controllers
{
    public class MuscleGroupController : Controller
    {
        private IFitChallengeDbContext context;

        public MuscleGroupController(IFitChallengeDbContext context) 
        {
            this.context = context;
        }

        public ActionResult Index() 
        {
            return View(context.MuscleGroups);
        }
    }
}