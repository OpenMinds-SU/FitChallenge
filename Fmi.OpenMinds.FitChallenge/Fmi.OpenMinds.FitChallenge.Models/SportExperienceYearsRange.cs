
using System.ComponentModel.DataAnnotations;
namespace Fmi.OpenMinds.FitChallenge.Models
{
    public enum  SportExperienceYearsRange
    {
        Unknown,
        [Display(Name = "0  year(s) ")]
        Zero,
        [Display(Name = "0< year(s) <=1")]
        LessThanOne,
        [Display(Name = "1< year(s) <=3")]
        OneToThree,
        [Display(Name = "3< year(s) <=5")]
        ThreeToFive,
        [Display(Name = "5< year(s) <=10")]
        FiveToTen,
        [Display(Name = "   year(s)  >10")]
        MoreThanTen
    }
}
