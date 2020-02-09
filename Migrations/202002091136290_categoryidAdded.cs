namespace TastyMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categoryidAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Recipes", "CategoryID", c => c.String());
            AddColumn("dbo.Recipes", "Category_Id", c => c.Guid());
            CreateIndex("dbo.Recipes", "Category_Id");
            AddForeignKey("dbo.Recipes", "Category_Id", "dbo.Categories", "Id");

            Sql(@"INSERT INTO [dbo].[Categories] ([Id], [Name]) VALUES (N'72bd504f-294b-ea11-89c4-001a7dda7113', N'Italian')
INSERT INTO [dbo].[Categories] ([Id], [Name]) VALUES (N'aa611558-294b-ea11-89c4-001a7dda7113', N'American')
INSERT INTO [dbo].[Categories] ([Id], [Name]) VALUES (N'ab611558-294b-ea11-89c4-001a7dda7113', N'Chinese')
INSERT INTO [dbo].[Categories] ([Id], [Name]) VALUES (N'd744b45e-294b-ea11-89c4-001a7dda7113', N'Iranian')
INSERT INTO [dbo].[Categories] ([Id], [Name]) VALUES (N'd844b45e-294b-ea11-89c4-001a7dda7113', N'BBQ')
INSERT INTO [dbo].[Categories] ([Id], [Name]) VALUES (N'4a9efd66-294b-ea11-89c4-001a7dda7113', N'Salads')
INSERT INTO [dbo].[Categories] ([Id], [Name]) VALUES (N'ee50226d-294b-ea11-89c4-001a7dda7113', N'Low carbs')
");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Recipes", new[] { "Category_Id" });
            DropColumn("dbo.Recipes", "Category_Id");
            DropColumn("dbo.Recipes", "CategoryID");
            DropTable("dbo.Categories");
        }
    }
}
