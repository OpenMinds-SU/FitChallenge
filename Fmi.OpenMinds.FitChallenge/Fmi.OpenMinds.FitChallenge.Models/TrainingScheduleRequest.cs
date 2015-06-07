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

        public SportExperienceYearsRange SportExperienceYearsRange { get; set; }

        public string Message { get; set; }

        public TrainingScheduleRequestState State { get; set; }

        public DateTime CreationDate { get; set; }

        public override string ToString()
        {
            return "Id: \t" + Id + "\n" +
                   "SportsmanId: \t\"" + SportsmanId + "\"\n" +
                   "Sportsman.Height: \t" + ((null != Sportsman) ? Sportsman.Height : null) + "\n" +
                   "Sportsman.Weight: \t" + ((null != Sportsman) ? Sportsman.Weight : null) + "\n" +
                   "Sportsman.Age: \t" + ((null != Sportsman) ? Sportsman.Age : null) + "\n" +
                   "InstructorId: \t\"" + InstructorId + "\"\n" +
                   "SportExperienceYearsRange: \t" + SportExperienceYearsRange + "\n" +
                   "Message: \t\"" + Message + "\"\n" +
                   "State: \t" + State + "\n" +
                   "CreationDate: \t" + CreationDate + "\n";
        }
    }
}
