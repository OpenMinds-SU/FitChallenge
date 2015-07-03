using Fmi.OpenMinds.FitChallenge.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Fmi.OpenMinds.FitChallenge.Web.Models
{
    public class WorkoutViewModel : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        public string UserId { get; set; }

        public virtual IList<WorkoutExercise> WorkoutExercises { get; set; }

        public virtual ICollection<MuscleGroupType> MuscleGroups { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(this.MuscleGroups == null || !this.MuscleGroups.Any())
            {
                yield return new ValidationResult("Please select at least one muscle group", new string[] { "MuscleGroups" });
            }
        }
    }
}