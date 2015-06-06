using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fmi.OpenMinds.FitChallenge.Models
{
    public class TrainingScheduleRequest
    {
        public TrainingScheduleRequest()
        {
        }

        public int Id { get; set; }

        public string SportsmanId { get; set; } // for Sportsman

        public virtual User Sportsman { get; set; }

        public string InstructorId { get; set; } // for Instructor

        public virtual User Instructor { get; set; }

        public int Height { get; set; } // TODO - Should I use Sportsman  property instead of defining here another one?
        
        public int Weight { get; set; } // TODO - Should I use Sportsman  property instead of defining here another one?

        public byte Age { get; set; } // TODO - Should I use Sportsman  property instead of defining here another one?

        public SportExperienceYearsRange SportExperienceYearsRange { get; set; }

        public string Message { get; set; }

        public TrainingScheduleRequestState State { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
