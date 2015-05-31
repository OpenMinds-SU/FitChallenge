using System.ComponentModel.DataAnnotations;
namespace Fmi.OpenMinds.FitChallenge.Models
{
    public class MeasurementValue
    {
        public int Id { get; set; }

        public MeasurementType MeasurementType { get; set; }

        [Range(0,300)]
        public double Value { get; set; }

        public int MeasurementId { get; set; }

        public virtual Measurement Measurement { get; set; }
    }
}
