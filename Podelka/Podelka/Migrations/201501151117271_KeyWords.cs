namespace Podelka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KeyWords : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "KeyWords", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "KeyWords");
        }
    }
}
