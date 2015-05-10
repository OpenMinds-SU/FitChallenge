using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fmi.OpenMinds.FitChallenge.Models
{
    public class WorkoutExercise
    {
        [Column(Order = 0), Key]
        public int WorkoutId { get; set; }

        public virtual Workout Workout { get; set; }

        [Column(Order = 1), Key]
        public int ExerciseId { get; set; }

        public virtual Exercise Exercise { get; set; }

        public int Sets { get; set; }

        public int Repeats { get; set; }

    }
}
