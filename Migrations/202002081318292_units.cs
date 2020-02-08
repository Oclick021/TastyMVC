namespace TastyMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class units : DbMigration
    {
        public override void Up()
        {
            
            Sql("INSERT INTO [dbo].[MeasurementUnits] ( [Name]) VALUES (N'teaspoon')");
            Sql("INSERT INTO [dbo].[MeasurementUnits] ( [Name]) VALUES (N'tablespoon')");
            Sql("INSERT INTO [dbo].[MeasurementUnits] ( [Name]) VALUES (N'fluid ounce')");
            Sql("INSERT INTO [dbo].[MeasurementUnits] ( [Name]) VALUES (N'cup')");
            Sql("INSERT INTO [dbo].[MeasurementUnits] ( [Name]) VALUES (N'pint')");
            Sql("INSERT INTO [dbo].[MeasurementUnits] ( [Name]) VALUES (N'ml')");
            Sql("INSERT INTO [dbo].[MeasurementUnits] ( [Name]) VALUES (N'l')");
            Sql("INSERT INTO [dbo].[MeasurementUnits] ( [Name]) VALUES (N'dl')");
            Sql("INSERT INTO [dbo].[MeasurementUnits] ( [Name]) VALUES (N'pound')");
            Sql("INSERT INTO [dbo].[MeasurementUnits] ( [Name]) VALUES (N'ounce')");
            Sql("INSERT INTO [dbo].[MeasurementUnits] ( [Name]) VALUES (N'mg')");
            Sql("INSERT INTO [dbo].[MeasurementUnits] ( [Name]) VALUES (N'g')");
            Sql("INSERT INTO [dbo].[MeasurementUnits] ( [Name]) VALUES (N'kg')");
        }

        public override void Down()
        {
        }
    }
}
