namespace Fmi.OpenMinds.FitChallenge.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RankModelUpdated : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Ranks", new[] { "Instructor_Id" });
            DropIndex("dbo.Ranks", new[] { "User_Id" });
            DropColumn("dbo.Ranks", "InstructorId");
            DropColumn("dbo.Ranks", "UserId");
            RenameColumn(table: "dbo.Ranks", name: "Instructor_Id", newName: "InstructorId");
            RenameColumn(table: "dbo.Ranks", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Ranks", "InstructorId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Ranks", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Ranks", "InstructorId");
            CreateIndex("dbo.Ranks", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Ranks", new[] { "UserId" });
            DropIndex("dbo.Ranks", new[] { "InstructorId" });
            AlterColumn("dbo.Ranks", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Ranks", "InstructorId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Ranks", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Ranks", name: "InstructorId", newName: "Instructor_Id");
            AddColumn("dbo.Ranks", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Ranks", "InstructorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Ranks", "User_Id");
            CreateIndex("dbo.Ranks", "Instructor_Id");
        }
    }
}
