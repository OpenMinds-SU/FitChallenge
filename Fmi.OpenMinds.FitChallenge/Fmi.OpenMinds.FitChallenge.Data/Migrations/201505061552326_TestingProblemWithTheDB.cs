namespace Fmi.OpenMinds.FitChallenge.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestingProblemWithTheDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WorkoutExercise1", "Workout_Id", "dbo.Workouts");
            DropForeignKey("dbo.WorkoutExercise1", "Exercise_Id", "dbo.Exercises");
            DropForeignKey("dbo.MuscleGroupWorkouts", "MuscleGroup_Id", "dbo.MuscleGroups");
            DropForeignKey("dbo.MuscleGroupWorkouts", "Workout_Id", "dbo.Workouts");
            DropIndex("dbo.WorkoutExercise1", new[] { "Workout_Id" });
            DropIndex("dbo.WorkoutExercise1", new[] { "Exercise_Id" });
            DropIndex("dbo.MuscleGroupWorkouts", new[] { "MuscleGroup_Id" });
            DropIndex("dbo.MuscleGroupWorkouts", new[] { "Workout_Id" });
            DropTable("dbo.MuscleGroupWorkouts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MuscleGroupWorkouts",
                c => new
                    {
                        MuscleGroup_Id = c.Int(nullable: false),
                        Workout_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MuscleGroup_Id, t.Workout_Id });
            
            CreateTable(
                "dbo.WorkoutExercise1",
                c => new
                    {
                        Workout_Id = c.Int(nullable: false),
                        Exercise_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Workout_Id, t.Exercise_Id });
            
            CreateIndex("dbo.MuscleGroupWorkouts", "Workout_Id");
            CreateIndex("dbo.MuscleGroupWorkouts", "MuscleGroup_Id");
            CreateIndex("dbo.WorkoutExercise1", "Exercise_Id");
            CreateIndex("dbo.WorkoutExercise1", "Workout_Id");
            AddForeignKey("dbo.MuscleGroupWorkouts", "Workout_Id", "dbo.Workouts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MuscleGroupWorkouts", "MuscleGroup_Id", "dbo.MuscleGroups", "Id", cascadeDelete: true);
            AddForeignKey("dbo.WorkoutExercise1", "Exercise_Id", "dbo.Exercises", "Id", cascadeDelete: true);
            AddForeignKey("dbo.WorkoutExercise1", "Workout_Id", "dbo.Workouts", "Id", cascadeDelete: true);
        }
    }
}
