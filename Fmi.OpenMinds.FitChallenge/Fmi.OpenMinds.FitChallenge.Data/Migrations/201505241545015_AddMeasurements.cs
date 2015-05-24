namespace Fmi.OpenMinds.FitChallenge.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMeasurements : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Measurements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MeasurementValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MeasurementType = c.Int(nullable: false),
                        Value = c.Double(nullable: false),
                        MeasurementId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Measurements", t => t.MeasurementId, cascadeDelete: true)
                .Index(t => t.MeasurementId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MeasurementValues", "MeasurementId", "dbo.Measurements");
            DropIndex("dbo.MeasurementValues", new[] { "MeasurementId" });
            DropTable("dbo.MeasurementValues");
            DropTable("dbo.Measurements");
        }
    }
}
