namespace Fmi.OpenMinds.FitChallenge.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Event
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public bool IsTrainingDone {get;set;}

        public string Supplements { get; set; }

        public bool SupplementsAreDrunken { get; set; }

        public string Food { get; set; }

        public int WorkoutId { get; set; }

        public virtual Workout Workout { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}