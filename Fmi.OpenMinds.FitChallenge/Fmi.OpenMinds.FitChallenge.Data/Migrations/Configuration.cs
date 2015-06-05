namespace Fmi.OpenMinds.FitChallenge.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Fmi.OpenMinds.FitChallenge.Data.FitChallengeDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Fmi.OpenMinds.FitChallenge.Data.FitChallengeDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            // Roles
            var roles = new HashSet<string>
            {
                "Instructor",
                "Sportsman"
            };

            foreach (var role in roles)
            {
                roleManager.Create(new IdentityRole { Name = role });
            }
        }
    }
}
