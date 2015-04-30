namespace Fmi.OpenMinds.FitChallenge.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMuscleGroupIdInExercise : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "MainMuscleGroupId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "MainMuscleGroupId");
        }
    }
}
