namespace TastyMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class recipePublishedAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "Published", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recipes", "Published");
        }
    }
}
