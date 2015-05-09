using Fmi.OpenMinds.FitChallenge.Data;
using Fmi.OpenMinds.FitChallenge.Models;
using System;
using System.Web.Mvc;

namespace Fmi.OpenMinds.FitChallenge.Web.Controllers
{
    [Authorize]
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
            
            return View(context.Workouts);
        }


        // GET : Workout/Create
        public ActionResult Create()
        {
            var workout = new Workout();
            
            ViewBag.MuscleGroupsAll = new SelectList(context.MuscleGroups, "Id", "Name");
            ViewBag.ExercisesAll = new SelectList(context.Exercises, "Id", "Name");
            return View(workout);
        }

        // POST : Workout/Create
        [HttpPost]
        public ActionResult Create(Workout workout, string MuscleGroupsAll, string ExercisesAll)
        {
            if (!ModelState.IsValid)
            {
                return View(workout);
            }

            var findMuscleGroup = context.MuscleGroups.Find(Int32.Parse(MuscleGroupsAll));
            var findExercise = context.Exercises.Find(Int32.Parse(ExercisesAll));

            workout.MuscleGroups.Add(findMuscleGroup);
            workout.Exercises.Add(findExercise);
            context.Workouts.Add(workout);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET : Workout/Edit
        public ActionResult Edit(int id)
        {
            var workoutEdit = context.Workouts.Find(id);
            return View(workoutEdit);
        }

        // POST : Workout/Edit
        [HttpPost]
        public ActionResult Edit(Workout workout)
        {
            if (!ModelState.IsValid)
            {
                return View(workout);
            }

            var workoutOld = context.Workouts.Find(workout.Id);
            workoutOld.Name = workout.Name;
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET : Workout/Delete
        public ActionResult Delete(int id)
        {
            return View(id);
        }

        // POST : Workout/Delete
        [HttpPost]
        public ActionResult Delete(Workout workout)
        {
            var workoutToDelete = context.Workouts.Find(workout.Id);
            context.Workouts.Remove(workoutToDelete);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}