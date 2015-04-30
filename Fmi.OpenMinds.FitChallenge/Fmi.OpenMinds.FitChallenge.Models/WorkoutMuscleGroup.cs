using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fmi.OpenMinds.FitChallenge.Models
{
    public class WorkoutMuscleGroup
    {
        public int Id { get; set; }

        public int WorkoutId { get; set; }

        public int MuscleGroupId { get; set; }
    }
}
