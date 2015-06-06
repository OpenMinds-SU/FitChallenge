﻿using System.Data.Entity;
using Fmi.OpenMinds.FitChallenge.Models;

namespace Fmi.OpenMinds.FitChallenge.Data
{
    public interface IFitChallengeDbContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Exercise> Exercises { get; set; }

        IDbSet<WorkoutMuscleGroup> WorkoutMuscleGroups { get; set; }

        IDbSet<WorkoutExercise> WorkoutExercises { get; set; }

        IDbSet<Workout> Workouts { get; set; }

        IDbSet<Event> Events { get; set; }

        IDbSet<Measurement> Measurements { get; set; }

        IDbSet<MeasurementValue> MeasurementValues { get; set; }

        IDbSet<TrainingScheduleRequest> TrainingScheduleRequests { get; set; }

        int SaveChanges();
    }
}