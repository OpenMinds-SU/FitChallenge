using System.Data.Entity;
using Fmi.OpenMinds.FitChallenge.Models;

namespace Fmi.OpenMinds.FitChallenge.Data
{
    public interface IFitChallengeDbContext
    {
        IDbSet<MuscleGroup> MuscleGroups { get; set; }

        IDbSet<Exercise> Exercises { get; set; }

        IDbSet<Workout> Workouts { get; set; }

        IDbSet<WorkoutMuscleGroup> WorkoutsMuscleGroups { get; set; }

        IDbSet<WorkoutExercise> WorkoutsExercises { get; set; }

        IDbSet<Event> Events { get; set; }

        int SaveChanges();
    }
}