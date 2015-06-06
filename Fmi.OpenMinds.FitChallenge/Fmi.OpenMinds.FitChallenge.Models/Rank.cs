
namespace Fmi.OpenMinds.FitChallenge.Models
{
    public class Rank
    {
        public int Id { get; set; }

        public int InstructorId { get; set; }

        public virtual User Instructor { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public int Value { get; set; }
    }
}
