using System.Data.Entity;
using Fmi.OpenMinds.FitChallenge.Models;

namespace Fmi.OpenMinds.FitChallenge.Data
{
    public interface IFitChallengeDbContext
    {
        IDbSet<MuscleGroup> MuscleGroups { get; set; }

        IDbSet<Exercise> Exercises { get; set; }

        int SaveChanges();
    }
}