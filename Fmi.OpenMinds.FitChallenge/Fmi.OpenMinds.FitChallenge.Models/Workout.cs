namespace Fmi.OpenMinds.FitChallenge.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Workout
    {
        public Workout()
        {
            this.Events = new HashSet<Event>();
            this.Exercises = new HashSet<Exercise>();
            this.MuscleGroups = new HashSet<MuscleGroup>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        public int UserId { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }

        public virtual ICollection<MuscleGroup> MuscleGroups { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
