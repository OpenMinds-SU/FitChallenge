using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fmi.OpenMinds.FitChallenge.Models
{
    public class Workout
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        public int UserId { get; set; }

        public List<MuscleGroup> MuscleGroup { get; set; }

        public List<Exercise> Exercises { get; set; }
    }
}
