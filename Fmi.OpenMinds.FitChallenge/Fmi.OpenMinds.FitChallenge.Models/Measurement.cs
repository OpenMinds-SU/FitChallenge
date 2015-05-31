using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Fmi.OpenMinds.FitChallenge.Models
{
    public class Measurement
    {
        public Measurement()
        {
            this.MeasurementValues = new HashSet<MeasurementValue>();
        }

        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<MeasurementValue> MeasurementValues { get; set; }

    }
}
