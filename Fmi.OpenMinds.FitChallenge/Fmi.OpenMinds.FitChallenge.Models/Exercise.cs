using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fmi.OpenMinds.FitChallenge.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        public string Url { get; set; }

        public int MainMuscleGroupId { get; set; }

        public List<Workout> Workout { get; set; }
    }
}
