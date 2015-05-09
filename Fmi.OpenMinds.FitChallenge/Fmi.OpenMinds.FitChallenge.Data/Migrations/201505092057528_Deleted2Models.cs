namespace Fmi.OpenMinds.FitChallenge.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Deleted2Models : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.WorkoutExercises");
            DropTable("dbo.WorkoutMuscleGroups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.WorkoutMuscleGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkoutId = c.Int(nullable: false),
                        MuscleGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
    }
}
