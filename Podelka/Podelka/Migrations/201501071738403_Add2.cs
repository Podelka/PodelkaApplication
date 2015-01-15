namespace Podelka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "DateRegistration", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "DateRegistration", c => c.DateTime(nullable: false));
        }
    }
}
