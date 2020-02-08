namespace TastyMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class recipeIdadded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RecipeSteps", "Recipe_Id", "dbo.Recipes");
            DropIndex("dbo.RecipeSteps", new[] { "Recipe_Id" });
            RenameColumn(table: "dbo.RecipeSteps", name: "Recipe_Id", newName: "RecipeID");
            AddColumn("dbo.RecipeSteps", "Title", c => c.String());
            AlterColumn("dbo.RecipeSteps", "RecipeID", c => c.Guid(nullable: false));
            CreateIndex("dbo.RecipeSteps", "RecipeID");
            AddForeignKey("dbo.RecipeSteps", "RecipeID", "dbo.Recipes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecipeSteps", "RecipeID", "dbo.Recipes");
            DropIndex("dbo.RecipeSteps", new[] { "RecipeID" });
            AlterColumn("dbo.RecipeSteps", "RecipeID", c => c.Guid());
            DropColumn("dbo.RecipeSteps", "Title");
            RenameColumn(table: "dbo.RecipeSteps", name: "RecipeID", newName: "Recipe_Id");
            CreateIndex("dbo.RecipeSteps", "Recipe_Id");
            AddForeignKey("dbo.RecipeSteps", "Recipe_Id", "dbo.Recipes", "Id");
        }
    }
}
