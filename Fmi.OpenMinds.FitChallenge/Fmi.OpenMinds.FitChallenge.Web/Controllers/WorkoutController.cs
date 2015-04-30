using Fmi.OpenMinds.FitChallenge.Data;
using Fmi.OpenMinds.FitChallenge.Models;
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

        public ActionResult Create()
        {
            var workout = new Workout();
            return View(workout);
        }

        [HttpPost]
        public ActionResult Create(Workout workout)
        {
            if (!ModelState.IsValid)
            {
                return View(workout);
            }

            context.Workouts.Add(workout);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}