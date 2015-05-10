namespace Fmi.OpenMinds.FitChallenge.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExerciseUserIdAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "UserId");
        }
    }
}
