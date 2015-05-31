namespace Fmi.OpenMinds.FitChallenge.Models
{
    public class MeasurementValue
    {
        public int Id { get; set; }

        public MeasurementType MeasurementType { get; set; }

        public double Value { get; set; }

        public int MeasurementId { get; set; }

        public virtual Measurement Measurement { get; set; }
    }
}
