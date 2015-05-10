namespace Fmi.OpenMinds.FitChallenge.Models
{
    public class WorkoutMuscleGroup
    {
        public int Id { get; set; }

        public int WorkoutId { get; set; }

        public virtual Workout Workout { get; set; }

        public MuscleGroupType MuscleGroup { get; set; }
    }
}
