namespace TastyMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class portionTypesChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Recipes", "portion", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recipes", "portion", c => c.String());
        }
    }
}
