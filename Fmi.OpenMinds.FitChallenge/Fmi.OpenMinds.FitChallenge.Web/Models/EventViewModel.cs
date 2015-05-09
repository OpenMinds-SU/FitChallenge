namespace Fmi.OpenMinds.FitChallenge.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class EventViewModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public bool IsTrainingDone { get; set; }

        [StringLength(200)]
        public string Supplements { get; set; }

        public bool SupplementsAreDrunken { get; set; }

        [StringLength(200)]
        public string Food { get; set; }

        public int WorkoutId { get; set; }

        public string UserId { get; set; }
    }
}