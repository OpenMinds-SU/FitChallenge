using System.Web.Mvc;
using System.Linq;
using Fmi.OpenMinds.FitChallenge.Data;
using Fmi.OpenMinds.FitChallenge.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace Fmi.OpenMinds.FitChallenge.Web.Controllers
{
    [Authorize]
    public class ExerciseController : Controller
    {
        private IFitChallengeDbContext context;

        public ExerciseController(IFitChallengeDbContext context) 
        {
            this.context = context;        
        }

        // Filtering The Exercises for The Current User
        public IQueryable<Exercise> GetCurrentUserExercises()
        {
            var currentUserId = User.Identity.GetUserId();
            var userExercises = context.Exercises
                .Where(ex => ex.UserId == currentUserId || ex.UserId == null);
            return userExercises;
        }
        
        public ActionResult Index()
        {
            return View(GetCurrentUserExercises());
        }

        // GET : Exercise/Create
        public ActionResult Create()
        {
            var exercise = new Exercise();
            return View("Edit", exercise);
        }

        // POST : Exercise/Create
        [HttpPost]
        public ActionResult Create(Exercise exercise)
        {
            if (exercise.MainMuscleGroup == MuscleGroupType.Unknown)
            {
                ModelState.AddModelError("MainMuscleGroup", "Please select muscle group.");
            }

            if (!ModelState.IsValid)
            {
                return View("Edit", exercise);
            }
            exercise.UserId = User.Identity.GetUserId();
            context.Exercises.Add(exercise);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET : Exercise/Edit
        public ActionResult Edit(int id)
        {
            var exerciseEdit = context.Exercises.Find(id);
            return View(exerciseEdit);
        }

        // POST : Exercise/Edit
        [HttpPost]
        public ActionResult Edit(Exercise exercise)
        {
            if (exercise.MainMuscleGroup == MuscleGroupType.Unknown)
            {
                ModelState.AddModelError("MainMuscleGroup", "Please select muscle group.");
            }

            if (!ModelState.IsValid)
            {
                return View(exercise);
            }

            var oldExercise = context.Exercises.Find(exercise.Id);
            oldExercise.Name = exercise.Name;
            oldExercise.Url = exercise.Url;
            oldExercise.MainMuscleGroup = exercise.MainMuscleGroup;
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET : Exercise/Delete
        public ActionResult Delete(int id)
        {
            return View(id);
        }

        // POST : Exercise/Delete
        [HttpPost]
        public ActionResult Delete(Exercise exercise)
        {
            var exerciseToDelete = context.Exercises.Find(exercise.Id);
            context.Exercises.Remove(exerciseToDelete);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}