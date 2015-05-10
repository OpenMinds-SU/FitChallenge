﻿namespace Fmi.OpenMinds.FitChallenge.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

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

        [Required]
        public int WorkoutId { get; set; }

        public string WorkoutName { get; set; }
    }
}