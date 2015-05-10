using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fmi.OpenMinds.FitChallenge.Models
{
    public class Exercise
    {
        public Exercise()
        {
            this.WorkoutExercises = new HashSet<WorkoutExercise>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        public string Url { get; set; }

        public MuscleGroupType MainMuscleGroup { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set;  }

        public virtual ICollection<WorkoutExercise> WorkoutExercises { get; set; }
    }
}
