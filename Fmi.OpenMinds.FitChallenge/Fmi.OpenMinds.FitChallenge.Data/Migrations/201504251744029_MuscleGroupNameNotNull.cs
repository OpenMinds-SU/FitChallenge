namespace Fmi.OpenMinds.FitChallenge.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MuscleGroupNameNotNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MuscleGroups", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MuscleGroups", "Name", c => c.String());
        }
    }
}
