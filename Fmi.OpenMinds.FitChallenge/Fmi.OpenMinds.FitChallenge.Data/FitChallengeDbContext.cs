namespace Fmi.OpenMinds.FitChallenge.Data
{
    using Fmi.OpenMinds.FitChallenge.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class FitChallengeDbContext : IdentityDbContext<User>
    {
        public FitChallengeDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static FitChallengeDbContext Create()
        {
            return new FitChallengeDbContext();
        }
    }

}
