using Fmi.OpenMinds.FitChallenge.Data;
using Fmi.OpenMinds.FitChallenge.Models;
using System;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;

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

        // Filtering The Workouts only for the Current User
        public ICollection<Workout> GetCurrentUserWorkouts()
        {
            var currentUser = User.Identity.GetUserId();
            ICollection<Workout> userWorkouts = new HashSet<Workout>();
            foreach (var workout in context.Workouts)
            {
                if (workout.UserId == currentUser)
                {
                    userWorkouts.Add(workout);
                }
            }
            return userWorkouts;
        }


        public ICollection<Exercise> GetCurrentUserExercises()
        {
            var currentUser = User.Identity.GetUserId();
            ICollection<Exercise> userExercises = new HashSet<Exercise>();
            foreach (var exercise in context.Exercises)
            {
                if (exercise.UserId == currentUser || exercise.UserId == "NULL")
                {
                    userExercises.Add(exercise);
                }
            }
            return userExercises;
        }

        // GET: Workout
        public ActionResult Index()
        {
            return View(GetCurrentUserWorkouts());
        }


        // GET : Workout/Create
        public ActionResult Create()
        {
            var workout = new Workout();
            
            ViewBag.ExercisesAll = new SelectList(GetCurrentUserExercises(), "Id", "Name");
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

            //var findMuscleGroup = context.MuscleGroups.Find(Int32.Parse(MuscleGroupsAll)); TODO
            var findExercise = context.Exercises.Find(Int32.Parse(ExercisesAll));

            //workout.MuscleGroups.Add(findMuscleGroup);
            //workout.Exercises.Add(findExercise);
            workout.UserId = User.Identity.GetUserId();
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