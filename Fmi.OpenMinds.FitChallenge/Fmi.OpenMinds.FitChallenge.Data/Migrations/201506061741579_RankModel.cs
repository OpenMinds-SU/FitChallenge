namespace Fmi.OpenMinds.FitChallenge.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RankModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ranks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InstructorId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Value = c.Int(nullable: false),
                        Instructor_Id = c.String(maxLength: 128),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Instructor_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Instructor_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ranks", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ranks", "Instructor_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Ranks", new[] { "User_Id" });
            DropIndex("dbo.Ranks", new[] { "Instructor_Id" });
            DropTable("dbo.Ranks");
        }
    }
}
