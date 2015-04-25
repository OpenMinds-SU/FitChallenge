namespace Fmi.OpenMinds.FitChallenge.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LengthValidationMuscleGroup : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MuscleGroups", "Name", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MuscleGroups", "Name", c => c.String(nullable: false));
        }
    }
}
