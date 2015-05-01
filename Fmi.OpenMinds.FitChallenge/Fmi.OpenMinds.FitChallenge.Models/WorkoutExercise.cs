
namespace Fmi.OpenMinds.FitChallenge.Models
{
    public class WorkoutExercise
    {
        public int Id { get; set; }

        public int WorkoutId { get; set; }

        public int ExerciseId { get; set;}

        public int Repeats { get; set; }

        public int Sets { get; set; }
    }
}
