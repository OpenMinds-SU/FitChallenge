﻿using Fmi.OpenMinds.FitChallenge.Data;
using Fmi.OpenMinds.FitChallenge.Models;
using System;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;
using Fmi.OpenMinds.FitChallenge.Web.Models;

namespace Fmi.OpenMinds.FitChallenge.Web.Controllers
{
    [Authorize]
    public class WorkoutController : BaseController
    {
        private IFitChallengeDbContext context;
        
        public WorkoutController(IFitChallengeDbContext context) 
        {
            this.context = context;        
        }

        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            var userWorkouts = this.context.Workouts
                .Where(w => w.UserId == currentUserId || w.UserId == null);

            return View(userWorkouts);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var workout = new WorkoutViewModel();
            GetUserExercises();
            return View(workout);
        }

        [HttpPost]
        public ActionResult Create(WorkoutViewModel model)
        {
            GetUserExercises();

            if (model.WorkoutExercises == null)
            {
                ModelState.AddModelError("WorkoutExercises", "Please add exercises");
                return View(model);
            }

            model.WorkoutExercises = model.WorkoutExercises
                .Where(e => e.Sets > 0 && e.Repeats > 0).ToList();

            if (!model.WorkoutExercises.Any()) 
            {
                ModelState.AddModelError("WorkoutExercises", "Please add valid exercises");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var workout = new Workout();
            workout.Name = model.Name;
            workout.UserId = User.Identity.GetUserId();
            context.Workouts.Add(workout);

            foreach (var musculeGroup in model.MuscleGroups)
            {
                context.WorkoutMuscleGroups.Add(new WorkoutMuscleGroup()
                {
                    Workout = workout,
                    MuscleGroup = musculeGroup
                });
            }

            foreach (var workoutExercise in model.WorkoutExercises)
            {
                workoutExercise.Workout = workout;
                context.WorkoutExercises.Add(workoutExercise);
            }

            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var workoutEdit = context.Workouts.Find(id);
            GetUserExercises();

            var workoutView = new WorkoutViewModel();
            workoutView.Name = workoutEdit.Name;
            workoutView.Id = workoutEdit.Id;
            workoutView.WorkoutExercises = workoutEdit.WorkoutExercises.ToList();
            workoutView.MuscleGroups = workoutEdit.WorkoutMuscleGroups.Select(item => item.MuscleGroup).ToList();
            return View(workoutView);
        }

        public class WorkoutExerciseEqualityComparer : IEqualityComparer<WorkoutExercise>
        {
            public bool Equals(WorkoutExercise x, WorkoutExercise y)
            {
                return x.ExerciseId == y.ExerciseId;
            }

            public int GetHashCode(WorkoutExercise obj)
            {
                return obj.ExerciseId.GetHashCode();
            }
        }

        [HttpPost]
        public ActionResult Edit(WorkoutViewModel model)
        {
            GetUserExercises();

            if (model.WorkoutExercises == null)
            {
                ModelState.AddModelError("WorkoutExercises", "Please add exercises");
                return View(model);
            }

            var disinctExercises = model.WorkoutExercises.Distinct(new WorkoutExerciseEqualityComparer());
            if (disinctExercises.Count() < model.WorkoutExercises.Count)
            {
                ModelState.AddModelError("WorkoutExercises", "Each exercise may exists only once for workout.");
                return View(model);
            }

            model.WorkoutExercises = model.WorkoutExercises
                .Where(e => e.Sets > 0 && e.Repeats > 0).ToList();

            if (!model.WorkoutExercises.Any())
            {
                ModelState.AddModelError("WorkoutExercises", "Please add valid exercises");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var workoutEditted = context.Workouts.Find(model.Id);
            workoutEditted.Name = model.Name;

            // Save muscule group changes.
            var groupsToRemove = workoutEditted.WorkoutMuscleGroups.ToList();
            groupsToRemove.ForEach(g => context.WorkoutMuscleGroups.Remove(g));

            foreach (var musculeGroup in model.MuscleGroups)
            {
                context.WorkoutMuscleGroups.Add(new WorkoutMuscleGroup()
                {
                    Workout = workoutEditted,
                    MuscleGroup = musculeGroup
                });
            }

            // Save workout exercises.
            var exercisesToRemove = workoutEditted.WorkoutExercises.ToList();
            exercisesToRemove.ForEach(ex => context.WorkoutExercises.Remove(ex));

            foreach (var workoutExercise in model.WorkoutExercises)
            {
                workoutExercise.Workout = workoutEditted;
                context.WorkoutExercises.Add(workoutExercise);
            }

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

        [NonAction]
        private void GetUserExercises()
        {
            var currentUserId = User.Identity.GetUserId();
            var userExercises = context.Exercises
                .Where(ex => ex.UserId == currentUserId || ex.UserId == null);
            ViewBag.Exercises = new SelectList(userExercises, "Id", "Name");
        }
    }
}