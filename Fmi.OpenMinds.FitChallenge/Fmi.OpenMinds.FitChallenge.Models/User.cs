﻿namespace Fmi.OpenMinds.FitChallenge.Models
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        public User()
            :base()
        {
            this.Events = new HashSet<Event>();
        }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public bool IsMale { get; set; }

        [StringLength(100)]
        public string PhoneNumber { get; set; }

        [MinLength(0)]
        [MaxLength(100)]
        public int ExperienceYears { get; set; }

        [MinLength(40)]
        [MaxLength(200)]
        public int Weight { get; set; }

        [MinLength(100)]
        [MaxLength(300)]
        public int Height { get; set; }

        [MinLength(18)]
        [MaxLength(100)]
        public int Age { get; set; }
        
        [StringLength(200)]
        public string Skype { get; set; }

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
