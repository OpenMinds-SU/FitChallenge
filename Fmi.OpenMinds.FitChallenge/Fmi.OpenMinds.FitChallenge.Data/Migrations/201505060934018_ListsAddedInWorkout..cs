namespace Fmi.OpenMinds.FitChallenge.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ListsAddedInWorkout : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "Workout_Id", c => c.Int());
            AddColumn("dbo.MuscleGroups", "Workout_Id", c => c.Int());
            CreateIndex("dbo.Exercises", "Workout_Id");
            CreateIndex("dbo.MuscleGroups", "Workout_Id");
            AddForeignKey("dbo.Exercises", "Workout_Id", "dbo.Workouts", "Id");
            AddForeignKey("dbo.MuscleGroups", "Workout_Id", "dbo.Workouts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MuscleGroups", "Workout_Id", "dbo.Workouts");
            DropForeignKey("dbo.Exercises", "Workout_Id", "dbo.Workouts");
            DropIndex("dbo.MuscleGroups", new[] { "Workout_Id" });
            DropIndex("dbo.Exercises", new[] { "Workout_Id" });
            DropColumn("dbo.MuscleGroups", "Workout_Id");
            DropColumn("dbo.Exercises", "Workout_Id");
        }
    }
}
