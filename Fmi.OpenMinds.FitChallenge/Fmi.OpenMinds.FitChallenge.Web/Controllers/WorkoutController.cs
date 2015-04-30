using Fmi.OpenMinds.FitChallenge.Data;
using System.Web.Mvc;

namespace Fmi.OpenMinds.FitChallenge.Web.Controllers
{
    public class WorkoutController : Controller
    {
        private IFitChallengeDbContext context;

        public WorkoutController(IFitChallengeDbContext context) 
        {
            this.context = context;        
        }
        // GET: Workout
        public ActionResult Index()
        {
            
            return View(context);
        }
    }
}