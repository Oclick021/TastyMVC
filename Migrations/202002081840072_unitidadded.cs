namespace TastyMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unitidadded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IngredientSizes", "Ingredient_Id", "dbo.Ingredients");
            DropForeignKey("dbo.IngredientSizes", "Unit_ID", "dbo.MeasurementUnits");
            DropIndex("dbo.IngredientSizes", new[] { "Ingredient_Id" });
            DropIndex("dbo.IngredientSizes", new[] { "Unit_ID" });
            RenameColumn(table: "dbo.IngredientSizes", name: "Ingredient_Id", newName: "IngredientID");
            RenameColumn(table: "dbo.IngredientSizes", name: "Unit_ID", newName: "UnitID");
            AlterColumn("dbo.IngredientSizes", "IngredientID", c => c.Guid(nullable: false));
            AlterColumn("dbo.IngredientSizes", "UnitID", c => c.Guid(nullable: false));
            CreateIndex("dbo.IngredientSizes", "UnitID");
            CreateIndex("dbo.IngredientSizes", "IngredientID");
            AddForeignKey("dbo.IngredientSizes", "IngredientID", "dbo.Ingredients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IngredientSizes", "UnitID", "dbo.MeasurementUnits", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IngredientSizes", "UnitID", "dbo.MeasurementUnits");
            DropForeignKey("dbo.IngredientSizes", "IngredientID", "dbo.Ingredients");
            DropIndex("dbo.IngredientSizes", new[] { "IngredientID" });
            DropIndex("dbo.IngredientSizes", new[] { "UnitID" });
            AlterColumn("dbo.IngredientSizes", "UnitID", c => c.Guid());
            AlterColumn("dbo.IngredientSizes", "IngredientID", c => c.Guid());
            RenameColumn(table: "dbo.IngredientSizes", name: "UnitID", newName: "Unit_ID");
            RenameColumn(table: "dbo.IngredientSizes", name: "IngredientID", newName: "Ingredient_Id");
            CreateIndex("dbo.IngredientSizes", "Unit_ID");
            CreateIndex("dbo.IngredientSizes", "Ingredient_Id");
            AddForeignKey("dbo.IngredientSizes", "Unit_ID", "dbo.MeasurementUnits", "ID");
            AddForeignKey("dbo.IngredientSizes", "Ingredient_Id", "dbo.Ingredients", "Id");
        }
    }
}
