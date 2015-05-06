using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fmi.OpenMinds.FitChallenge.Models
{
    public class MuscleGroup
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength=3)]
        public string Name { get; set; }

        public List<Workout> Workout { get; set; }
    }
}
