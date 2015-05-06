using Fmi.OpenMinds.FitChallenge.Data;
using Fmi.OpenMinds.FitChallenge.Models;
using System.Collections.Generic;
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
            List<SelectListItem> muscleGroups = new List<SelectListItem>();
            
            foreach (var muscleGroup in context.MuscleGroups)
            {
                muscleGroups.Add(new SelectListItem { Text = muscleGroup.Name, Value = muscleGroup.Id.ToString() });
            }
            ViewBag.DDLMuscleGroups = muscleGroups;
            return View(workout);
        }

        [HttpPost]
        public ActionResult Create(Workout workout) 
        {
            System.Console.Out.WriteLine();
            System.Console.WriteLine("Workoutresults"+workout);
            return View();
        }
        

    }
}