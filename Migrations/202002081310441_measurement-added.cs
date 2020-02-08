namespace TastyMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class measurementadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MeasurementUnits",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MeasurementUnits");
        }
    }
}
