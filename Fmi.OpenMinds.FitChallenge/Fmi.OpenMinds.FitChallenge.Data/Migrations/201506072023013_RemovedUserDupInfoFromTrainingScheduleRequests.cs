namespace Fmi.OpenMinds.FitChallenge.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedUserDupInfoFromTrainingScheduleRequests : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TrainingScheduleRequests", "Height");
            DropColumn("dbo.TrainingScheduleRequests", "Weight");
            DropColumn("dbo.TrainingScheduleRequests", "Age");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TrainingScheduleRequests", "Age", c => c.Byte(nullable: false));
            AddColumn("dbo.TrainingScheduleRequests", "Weight", c => c.Int(nullable: false));
            AddColumn("dbo.TrainingScheduleRequests", "Height", c => c.Int(nullable: false));
        }
    }
}
