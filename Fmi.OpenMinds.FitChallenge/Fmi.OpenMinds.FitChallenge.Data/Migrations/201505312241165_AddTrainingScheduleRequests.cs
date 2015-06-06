namespace Fmi.OpenMinds.FitChallenge.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTrainingScheduleRequests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrainingScheduleRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SportsmanId = c.String(),
                        InstructorId = c.String(),
                        Height = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        Age = c.Byte(nullable: false),
                        SportExperienceYearsRange = c.Int(nullable: false),
                        Message = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TrainingScheduleRequests");
        }
    }
}
