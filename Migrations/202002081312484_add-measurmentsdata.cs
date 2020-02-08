namespace TastyMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmeasurmentsdata : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IngredientSizes", "Unit_ID", c => c.Guid());
            CreateIndex("dbo.IngredientSizes", "Unit_ID");
            AddForeignKey("dbo.IngredientSizes", "Unit_ID", "dbo.MeasurementUnits", "ID");
            DropColumn("dbo.IngredientSizes", "Unit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IngredientSizes", "Unit", c => c.Int(nullable: false));
            DropForeignKey("dbo.IngredientSizes", "Unit_ID", "dbo.MeasurementUnits");
            DropIndex("dbo.IngredientSizes", new[] { "Unit_ID" });
            DropColumn("dbo.IngredientSizes", "Unit_ID");
        }
    }
}
