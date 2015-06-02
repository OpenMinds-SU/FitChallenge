namespace Fmi.OpenMinds.FitChallenge.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class InstructorViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [MinLength(0)]
        [MaxLength(100)]
        public int ExperienceYears { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [StringLength(200)]
        public string Skype { get; set; }

        [StringLength(2000)]
        [DataType(DataType.MultilineText)]
        public string AdditionalInformation { get; set; }
    }
}