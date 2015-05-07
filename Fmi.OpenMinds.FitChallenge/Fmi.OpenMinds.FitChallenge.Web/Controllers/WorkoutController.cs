using Fmi.OpenMinds.FitChallenge.Data;
using Fmi.OpenMinds.FitChallenge.Models;
using System;
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
            
            ViewBag.MuscleGroupsAll = new SelectList(context.MuscleGroups, "Id", "Name");
            ViewBag.ExercisesAll = new SelectList(context.Exercises, "Id", "Name");
            return View(workout);
        }

        [HttpPost]
        public ContentResult Create(Workout workout, string MuscleGroupsAll, string ExercisesAll)
        {
            if (!ModelState.IsValid)
            {
                //return View(workout);
            }

            MuscleGroup muscleGroup = context.MuscleGroups.Find(Int32.Parse(MuscleGroupsAll));
            Exercise exercise = context.Exercises.Find(Int32.Parse(ExercisesAll));
            //workout.MuscleGroups.Add(muscleGroup);
            //workout.Exercises.Add(exercise);
            //context.Workouts.Add(workout);
            //context.SaveChanges();
            return Content(workout.Name + " " + workout.MuscleGroups + muscleGroup + "  " + workout.Exercises + exercise + "  " + MuscleGroupsAll + "  " + ExercisesAll);
            //return RedirectToAction("Index");
        }

    }
}