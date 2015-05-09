namespace Fmi.OpenMinds.FitChallenge.Data
{
    using System.Data.Entity;
    using Fmi.OpenMinds.FitChallenge.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class FitChallengeDbContext : IdentityDbContext<User>, IFitChallengeDbContext
    {
        public FitChallengeDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static FitChallengeDbContext Create()
        {
            return new FitChallengeDbContext();
        }

        public IDbSet<MuscleGroup> MuscleGroups { get; set; }

        public IDbSet<Exercise> Exercises { get; set; }

        public IDbSet<Workout> Workouts { get; set; }

        public IDbSet<WorkoutMuscleGroup> WorkoutsMuscleGroups { get; set; }

        public IDbSet<WorkoutExercise> WorkoutsExercises { get; set; }

        public IDbSet<Event> Events { get; set; }
    }

}
