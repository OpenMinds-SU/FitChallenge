namespace Fmi.OpenMinds.FitChallenge.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Workout
    {
        public Workout()
        {
            this.Events = new HashSet<Event>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        public int UserId { get; set; }

        public List<Exercise> Exercises { get; set; }

        public List<MuscleGroup> MuscleGroups { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
