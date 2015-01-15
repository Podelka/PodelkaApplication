namespace Podelka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmig : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Price", c => c.Double());
            AlterColumn("dbo.Products", "PriceDiscount", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "PriceDiscount", c => c.Double(nullable: false));
            AlterColumn("dbo.Products", "Price", c => c.Double(nullable: false));
        }
    }
}
