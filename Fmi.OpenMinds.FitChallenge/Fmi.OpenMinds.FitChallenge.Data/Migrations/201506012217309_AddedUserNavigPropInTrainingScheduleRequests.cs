namespace Fmi.OpenMinds.FitChallenge.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserNavigPropInTrainingScheduleRequests : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TrainingScheduleRequests", "State", c => c.Int(nullable: false));
            AddColumn("dbo.TrainingScheduleRequests", "CreationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrainingScheduleRequests", "SportsmanId", c => c.String(maxLength: 128));
            AlterColumn("dbo.TrainingScheduleRequests", "InstructorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.TrainingScheduleRequests", "SportsmanId");
            CreateIndex("dbo.TrainingScheduleRequests", "InstructorId");
            AddForeignKey("dbo.TrainingScheduleRequests", "InstructorId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.TrainingScheduleRequests", "SportsmanId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.TrainingScheduleRequests", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TrainingScheduleRequests", "Date", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.TrainingScheduleRequests", "SportsmanId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TrainingScheduleRequests", "InstructorId", "dbo.AspNetUsers");
            DropIndex("dbo.TrainingScheduleRequests", new[] { "InstructorId" });
            DropIndex("dbo.TrainingScheduleRequests", new[] { "SportsmanId" });
            AlterColumn("dbo.TrainingScheduleRequests", "InstructorId", c => c.String());
            AlterColumn("dbo.TrainingScheduleRequests", "SportsmanId", c => c.String());
            DropColumn("dbo.TrainingScheduleRequests", "CreationDate");
            DropColumn("dbo.TrainingScheduleRequests", "State");
        }
    }
}
