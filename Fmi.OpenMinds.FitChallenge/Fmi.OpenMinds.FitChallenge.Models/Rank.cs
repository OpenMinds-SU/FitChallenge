
namespace Fmi.OpenMinds.FitChallenge.Models
{
    public class Rank
    {
        public int Id { get; set; }

        public string InstructorId { get; set; }

        public virtual User Instructor { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int Value { get; set; }
    }
}
