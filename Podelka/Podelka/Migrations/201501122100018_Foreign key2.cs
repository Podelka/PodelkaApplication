namespace Podelka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Foreignkey2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "ProductGenderTypeId", "dbo.GenderTypeProducts");
            DropIndex("dbo.Products", new[] { "ProductGenderTypeId" });
            AlterColumn("dbo.Products", "ProductGenderTypeId", c => c.Byte());
            CreateIndex("dbo.Products", "ProductGenderTypeId");
            AddForeignKey("dbo.Products", "ProductGenderTypeId", "dbo.GenderTypeProducts", "ProductGenderTypeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProductGenderTypeId", "dbo.GenderTypeProducts");
            DropIndex("dbo.Products", new[] { "ProductGenderTypeId" });
            AlterColumn("dbo.Products", "ProductGenderTypeId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Products", "ProductGenderTypeId");
            AddForeignKey("dbo.Products", "ProductGenderTypeId", "dbo.GenderTypeProducts", "ProductGenderTypeId", cascadeDelete: true);
        }
    }
}
