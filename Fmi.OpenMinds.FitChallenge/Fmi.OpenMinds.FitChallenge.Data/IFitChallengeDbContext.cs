﻿using System.Data.Entity;
using Fmi.OpenMinds.FitChallenge.Models;

namespace Fmi.OpenMinds.FitChallenge.Data
{
    public interface IFitChallengeDbContext
    {
        IDbSet<Exercise> Exercises { get; set; }

        IDbSet<Workout> Workouts { get; set; }

        IDbSet<Event> Events { get; set; }

        int SaveChanges();
    }
}