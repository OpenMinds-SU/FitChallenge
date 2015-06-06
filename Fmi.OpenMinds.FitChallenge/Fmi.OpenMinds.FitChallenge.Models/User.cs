namespace Fmi.OpenMinds.FitChallenge.Models
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User : IdentityUser
    {
        public User()
            : base()
        {
            this.Events = new HashSet<Event>();
        }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public bool? IsMale { get; set; }

        [StringLength(100)]
        public override string PhoneNumber { get; set; }

        [Range(0, 100)]
        public int? ExperienceYears { get; set; }

        [Range(40, 200)]
        public int? Weight { get; set; }

        [Range(100, 300)]
        public int? Height { get; set; }

        [Range(18, 100)]
        public int? Age { get; set; }

        [StringLength(200)]
        public string Skype { get; set; }

        [NotMapped]
        public double Rank { get; set; }

        [StringLength(2000)]
        public string AdditionalInformation { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
