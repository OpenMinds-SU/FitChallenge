namespace Fmi.OpenMinds.FitChallenge.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Workout
    {
        public Workout()
        {
            this.Events = new HashSet<Event>();
            this.WorkoutExercises = new HashSet<WorkoutExercise>();
            this.WorkoutMuscleGroups = new HashSet<WorkoutMuscleGroup>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<WorkoutExercise> WorkoutExercises { get; set; }

        public virtual ICollection<WorkoutMuscleGroup> WorkoutMuscleGroups { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
