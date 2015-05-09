namespace Fmi.OpenMinds.FitChallenge.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddListsInModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkoutsExercises",
                c => new
                    {
                        Workout_Id = c.Int(nullable: false),
                        Exercise_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Workout_Id, t.Exercise_Id })
                .ForeignKey("dbo.Workouts", t => t.Workout_Id, cascadeDelete: true)
                .ForeignKey("dbo.Exercises", t => t.Exercise_Id, cascadeDelete: true)
                .Index(t => t.Workout_Id)
                .Index(t => t.Exercise_Id);
            
            CreateTable(
                "dbo.MuscleGroupWorkouts",
                c => new
                    {
                        MuscleGroup_Id = c.Int(nullable: false),
                        Workout_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MuscleGroup_Id, t.Workout_Id })
                .ForeignKey("dbo.MuscleGroups", t => t.MuscleGroup_Id, cascadeDelete: true)
                .ForeignKey("dbo.Workouts", t => t.Workout_Id, cascadeDelete: true)
                .Index(t => t.MuscleGroup_Id)
                .Index(t => t.Workout_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MuscleGroupWorkouts", "Workout_Id", "dbo.Workouts");
            DropForeignKey("dbo.MuscleGroupWorkouts", "MuscleGroup_Id", "dbo.MuscleGroups");
            DropForeignKey("dbo.WorkoutsExercises", "Exercise_Id", "dbo.Exercises");
            DropForeignKey("dbo.WorkoutsExercises", "Workout_Id", "dbo.Workouts");
            DropIndex("dbo.MuscleGroupWorkouts", new[] { "Workout_Id" });
            DropIndex("dbo.MuscleGroupWorkouts", new[] { "MuscleGroup_Id" });
            DropIndex("dbo.WorkoutsExercises", new[] { "Exercise_Id" });
            DropIndex("dbo.WorkoutsExercises", new[] { "Workout_Id" });
            DropTable("dbo.MuscleGroupWorkouts");
            DropTable("dbo.WorkoutsExercises");
        }
    }
}
