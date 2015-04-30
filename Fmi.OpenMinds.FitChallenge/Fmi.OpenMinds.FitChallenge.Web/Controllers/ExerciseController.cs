using System.Web.Mvc;
using Fmi.OpenMinds.FitChallenge.Data;
using Fmi.OpenMinds.FitChallenge.Models;

namespace Fmi.OpenMinds.FitChallenge.Web.Controllers
{
    public class ExerciseController : Controller
    {
        private IFitChallengeDbContext context;

        public ExerciseController(IFitChallengeDbContext context) 
        {
            this.context = context;        
        }
        
        public ActionResult Index()
        {
            return View(context.Exercises);
        }

    }
}