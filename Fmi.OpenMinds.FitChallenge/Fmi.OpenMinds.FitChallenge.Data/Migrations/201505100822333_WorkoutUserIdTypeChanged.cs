namespace Fmi.OpenMinds.FitChallenge.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkoutUserIdTypeChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Workouts", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Workouts", "UserId", c => c.Int(nullable: false));
        }
    }
}
