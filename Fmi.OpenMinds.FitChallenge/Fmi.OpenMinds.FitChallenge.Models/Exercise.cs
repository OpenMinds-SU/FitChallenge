using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fmi.OpenMinds.FitChallenge.Models
{
    public class Exercise
    {
        public Exercise()
        {
            this.Workout = new HashSet<Workout>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        public string Url { get; set; }

        public int MainMuscleGroupId { get; set; }

        public string UserId { get; set; }

        public ICollection<Workout> Workout { get; set; }
    }
}
