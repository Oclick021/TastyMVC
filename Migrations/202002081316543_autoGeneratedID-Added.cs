namespace TastyMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class autoGeneratedIDAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IngredientSizes", "Ingredient_Id", "dbo.Ingredients");
            DropForeignKey("dbo.IngredientSizes", "Unit_ID", "dbo.MeasurementUnits");
            DropForeignKey("dbo.IngredientSizes", "Recipe_Id", "dbo.Recipes");
            DropForeignKey("dbo.RecipeSteps", "Recipe_Id", "dbo.Recipes");
            DropForeignKey("dbo.RecipeSteps", "Image_Id", "dbo.Images");
            DropForeignKey("dbo.Recipes", "Thumbnail_Id", "dbo.Images");
            DropPrimaryKey("dbo.Ingredients");
            DropPrimaryKey("dbo.MeasurementUnits");
            DropPrimaryKey("dbo.Recipes");
            DropPrimaryKey("dbo.IngredientSizes");
            DropPrimaryKey("dbo.RecipeSteps");
            DropPrimaryKey("dbo.Images");
            AlterColumn("dbo.Ingredients", "Id", c => c.Guid(nullable: false,identity: true, defaultValueSql: "newsequentialid()"));
            AlterColumn("dbo.MeasurementUnits", "ID", c => c.Guid(nullable: false,identity: true, defaultValueSql: "newsequentialid()"));
            AlterColumn("dbo.Recipes", "Id", c => c.Guid(nullable: false,identity: true, defaultValueSql: "newsequentialid()"));
            AlterColumn("dbo.IngredientSizes", "Id", c => c.Guid(nullable: false,identity: true, defaultValueSql: "newsequentialid()"));
            AlterColumn("dbo.RecipeSteps", "ID", c => c.Guid(nullable: false,identity: true, defaultValueSql: "newsequentialid()"));
            AlterColumn("dbo.Images", "Id", c => c.Guid(nullable: false,identity: true, defaultValueSql: "newsequentialid()"));
            AddPrimaryKey("dbo.Ingredients", "Id");
            AddPrimaryKey("dbo.MeasurementUnits", "ID");
            AddPrimaryKey("dbo.Recipes", "Id");
            AddPrimaryKey("dbo.IngredientSizes", "Id");
            AddPrimaryKey("dbo.RecipeSteps", "ID");
            AddPrimaryKey("dbo.Images", "Id");
            AddForeignKey("dbo.IngredientSizes", "Ingredient_Id", "dbo.Ingredients", "Id");
            AddForeignKey("dbo.IngredientSizes", "Unit_ID", "dbo.MeasurementUnits", "ID");
            AddForeignKey("dbo.IngredientSizes", "Recipe_Id", "dbo.Recipes", "Id");
            AddForeignKey("dbo.RecipeSteps", "Recipe_Id", "dbo.Recipes", "Id");
            AddForeignKey("dbo.RecipeSteps", "Image_Id", "dbo.Images", "Id");
            AddForeignKey("dbo.Recipes", "Thumbnail_Id", "dbo.Images", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "Thumbnail_Id", "dbo.Images");
            DropForeignKey("dbo.RecipeSteps", "Image_Id", "dbo.Images");
            DropForeignKey("dbo.RecipeSteps", "Recipe_Id", "dbo.Recipes");
            DropForeignKey("dbo.IngredientSizes", "Recipe_Id", "dbo.Recipes");
            DropForeignKey("dbo.IngredientSizes", "Unit_ID", "dbo.MeasurementUnits");
            DropForeignKey("dbo.IngredientSizes", "Ingredient_Id", "dbo.Ingredients");
            DropPrimaryKey("dbo.Images");
            DropPrimaryKey("dbo.RecipeSteps");
            DropPrimaryKey("dbo.IngredientSizes");
            DropPrimaryKey("dbo.Recipes");
            DropPrimaryKey("dbo.MeasurementUnits");
            DropPrimaryKey("dbo.Ingredients");
            AlterColumn("dbo.Images", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.RecipeSteps", "ID", c => c.Guid(nullable: false));
            AlterColumn("dbo.IngredientSizes", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Recipes", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.MeasurementUnits", "ID", c => c.Guid(nullable: false));
            AlterColumn("dbo.Ingredients", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Images", "Id");
            AddPrimaryKey("dbo.RecipeSteps", "ID");
            AddPrimaryKey("dbo.IngredientSizes", "Id");
            AddPrimaryKey("dbo.Recipes", "Id");
            AddPrimaryKey("dbo.MeasurementUnits", "ID");
            AddPrimaryKey("dbo.Ingredients", "Id");
            AddForeignKey("dbo.Recipes", "Thumbnail_Id", "dbo.Images", "Id");
            AddForeignKey("dbo.RecipeSteps", "Image_Id", "dbo.Images", "Id");
            AddForeignKey("dbo.RecipeSteps", "Recipe_Id", "dbo.Recipes", "Id");
            AddForeignKey("dbo.IngredientSizes", "Recipe_Id", "dbo.Recipes", "Id");
            AddForeignKey("dbo.IngredientSizes", "Unit_ID", "dbo.MeasurementUnits", "ID");
            AddForeignKey("dbo.IngredientSizes", "Ingredient_Id", "dbo.Ingredients", "Id");
        }
    }
}