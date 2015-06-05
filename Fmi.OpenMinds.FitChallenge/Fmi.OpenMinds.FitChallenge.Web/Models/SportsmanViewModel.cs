namespace Fmi.OpenMinds.FitChallenge.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SportsmanViewModel
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
        public bool IsMale { get; set; }

        [StringLength(100)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Range(40, 200)]
        public int Weight { get; set; }

        [Range(100, 300)]
        public int Height { get; set; }

        [Range(18, 100)]
        public int Age { get; set; }

        [StringLength(200)]
        public string Skype { get; set; }

        [StringLength(2000)]
        [DataType(DataType.MultilineText)]
        public string AdditionalInformation { get; set; }
    }
}