namespace TastyMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdByFixed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Recipes", "CreatedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Recipes", new[] { "CreatedBy_Id" });
            AddColumn("dbo.Recipes", "CreatedBy", c => c.String());
            DropColumn("dbo.Recipes", "CreatedBy_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Recipes", "CreatedBy_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Recipes", "CreatedBy");
            CreateIndex("dbo.Recipes", "CreatedBy_Id");
            AddForeignKey("dbo.Recipes", "CreatedBy_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
