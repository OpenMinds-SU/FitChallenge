using System.ComponentModel.DataAnnotations;
namespace Fmi.OpenMinds.FitChallenge.Models
{
    public enum MeasurementType
    {
        Weight,
        Neck,
        [Display(Name = "Left arm")]
        LeftArm,
        [Display(Name = "Right arm")]
        RightArm,
        Triceps,
        Waist,
        Hips,
        Thighs,
        Calfs
    }
}
