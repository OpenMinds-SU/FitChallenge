namespace Fmi.OpenMinds.FitChallenge.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWorkoutExercise : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkoutExercises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkoutId = c.Int(nullable: false),
                        ExerciseId = c.Int(nullable: false),
                        Repeats = c.Int(nullable: false),
                        Sets = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WorkoutExercises");
        }
    }
}
