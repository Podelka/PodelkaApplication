namespace Podelka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changelastadd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductMaterials", "MaterialId", "dbo.Materials");
            DropIndex("dbo.ProductMaterials", new[] { "MaterialId" });
            DropPrimaryKey("dbo.ProductMaterials");
            DropPrimaryKey("dbo.Materials");
            AddColumn("dbo.Categories", "Gender", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "CountGood", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "CountMedium", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "CountBad", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "ResultRating", c => c.Double(nullable: false));
            AddColumn("dbo.Workrooms", "ResultRating", c => c.Double(nullable: false));
            AddColumn("dbo.Workrooms", "Background", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Products", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Products", "PriceDiscount", c => c.Double(nullable: false));
            AlterColumn("dbo.ProductMaterials", "MaterialId", c => c.Short(nullable: false));
            AlterColumn("dbo.Materials", "MaterialId", c => c.Short(nullable: false, identity: true));
            AlterColumn("dbo.Workrooms", "CountGood", c => c.Int(nullable: false));
            AlterColumn("dbo.Workrooms", "CountMedium", c => c.Int(nullable: false));
            AlterColumn("dbo.Workrooms", "CountBad", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ProductMaterials", new[] { "ProductId", "MaterialId" });
            AddPrimaryKey("dbo.Materials", "MaterialId");
            CreateIndex("dbo.ProductMaterials", "MaterialId");
            AddForeignKey("dbo.ProductMaterials", "MaterialId", "dbo.Materials", "MaterialId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductMaterials", "MaterialId", "dbo.Materials");
            DropIndex("dbo.ProductMaterials", new[] { "MaterialId" });
            DropPrimaryKey("dbo.Materials");
            DropPrimaryKey("dbo.ProductMaterials");
            AlterColumn("dbo.Workrooms", "CountBad", c => c.Short(nullable: false));
            AlterColumn("dbo.Workrooms", "CountMedium", c => c.Short(nullable: false));
            AlterColumn("dbo.Workrooms", "CountGood", c => c.Short(nullable: false));
            AlterColumn("dbo.Materials", "MaterialId", c => c.Byte(nullable: false));
            AlterColumn("dbo.ProductMaterials", "MaterialId", c => c.Byte(nullable: false));
            AlterColumn("dbo.Products", "PriceDiscount", c => c.Single(nullable: false));
            AlterColumn("dbo.Products", "Price", c => c.Single(nullable: false));
            DropColumn("dbo.Workrooms", "Background");
            DropColumn("dbo.Workrooms", "ResultRating");
            DropColumn("dbo.Products", "ResultRating");
            DropColumn("dbo.Products", "CountBad");
            DropColumn("dbo.Products", "CountMedium");
            DropColumn("dbo.Products", "CountGood");
            DropColumn("dbo.Categories", "Gender");
            AddPrimaryKey("dbo.Materials", "MaterialId");
            AddPrimaryKey("dbo.ProductMaterials", new[] { "ProductId", "MaterialId" });
            CreateIndex("dbo.ProductMaterials", "MaterialId");
            AddForeignKey("dbo.ProductMaterials", "MaterialId", "dbo.Materials", "MaterialId", cascadeDelete: true);
        }
    }
}
