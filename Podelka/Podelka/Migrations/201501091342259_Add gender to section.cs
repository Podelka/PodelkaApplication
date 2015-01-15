namespace Podelka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addgendertosection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sections", "Gender", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sections", "Gender");
        }
    }
}
