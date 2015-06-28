using Fmi.OpenMinds.FitChallenge.Data;
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

        // GET : Workout/Edit
        public ActionResult Edit(int id)
        {
            var workoutEdit = context.Workouts.Find(id);
            GetUserExercises();

            var workoutView = new WorkoutViewModel();
            workoutView.Name = workoutEdit.Name;
            workoutView.Id = workoutEdit.Id;
            workoutView.WorkoutExercises = workoutEdit.WorkoutExercises;

            return View(workoutView);
        }

        // POST : Workout/Edit
        [HttpPost]
        public ActionResult Edit(WorkoutViewModel model)
        {
            //TODO : Shoud be fixed
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


            // MINE

            var workoutEditted = context.Workouts.Find(model.Id);




            //var workoutOld = context.Workouts.Find(workout.Id);
            //workoutOld.Name = workout.Name;

            //FROME CREATE

            //var workout = new Workout();
            
            //workout.UserId = User.Identity.GetUserId();
            //context.Workouts.Add(workout);

            foreach (var muscleGroup in workoutEditted.WorkoutMuscleGroups)
            {
                context.WorkoutMuscleGroups.Remove(muscleGroup);
            }


            foreach (var workoutExercise in workoutEditted.WorkoutExercises)
            {
                context.WorkoutExercises.Remove(workoutExercise);
            }



            workoutEditted.Name = model.Name;



            foreach (var musculeGroup in model.MuscleGroups)
            {
                context.WorkoutMuscleGroups.Add(new WorkoutMuscleGroup()
                {
                    Workout = workoutEditted,
                    MuscleGroup = musculeGroup
                });
            }

            //foreach (var muscleGroupModel in model.MuscleGroups)
            //{
            //    foreach (var muscleGroup in workoutEditted.WorkoutMuscleGroups)
            //    {
            //        if (muscleGroup.MuscleGroup != muscleGroupModel)
            //        {
            //            context.WorkoutMuscleGroups.Add(new WorkoutMuscleGroup()
            //            {
            //                Workout = workoutEditted,
            //                MuscleGroup = muscleGroupModel
            //            });
            //        }
            //        else if(muscleGroup.MuscleGroup == muscleGroupModel)
            //        {
            //            muscleGroup.Workout = workoutEditted;
            //        }
            //    }
            // }


            

            foreach (var workoutExercise in model.WorkoutExercises)
            {
                workoutExercise.Workout = workoutEditted;
                context.WorkoutExercises.Add(workoutExercise);
            }

            //foreach (var workoutExerciseModel in model.WorkoutExercises)
            //{
            //    foreach (var workoutExercise in workoutEditted.WorkoutExercises)
            //    {
            //        if (workoutExercise.Exercise.Name == workoutExerciseModel.Exercise.Name)
            //        {
            //            workoutExercise.Sets = workoutExerciseModel.Sets;
            //            workoutExercise.Repeats = workoutExercise.Repeats;
            //        }
            //        else
            //        {
            //            workoutExerciseModel.Workout = workoutEditted;
            //            context.WorkoutExercises.Add(workoutExerciseModel);
            //        }
                    
            //    }
                
            //}

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