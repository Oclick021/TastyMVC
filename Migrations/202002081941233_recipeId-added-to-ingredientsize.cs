namespace TastyMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class recipeIdaddedtoingredientsize : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IngredientSizes", "Recipe_Id", "dbo.Recipes");
            DropIndex("dbo.IngredientSizes", new[] { "Recipe_Id" });
            RenameColumn(table: "dbo.IngredientSizes", name: "Recipe_Id", newName: "RecipeId");
            AlterColumn("dbo.IngredientSizes", "RecipeId", c => c.Guid(nullable: false));
            CreateIndex("dbo.IngredientSizes", "RecipeId");
            AddForeignKey("dbo.IngredientSizes", "RecipeId", "dbo.Recipes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IngredientSizes", "RecipeId", "dbo.Recipes");
            DropIndex("dbo.IngredientSizes", new[] { "RecipeId" });
            AlterColumn("dbo.IngredientSizes", "RecipeId", c => c.Guid());
            RenameColumn(table: "dbo.IngredientSizes", name: "RecipeId", newName: "Recipe_Id");
            CreateIndex("dbo.IngredientSizes", "Recipe_Id");
            AddForeignKey("dbo.IngredientSizes", "Recipe_Id", "dbo.Recipes", "Id");
        }
    }
}
