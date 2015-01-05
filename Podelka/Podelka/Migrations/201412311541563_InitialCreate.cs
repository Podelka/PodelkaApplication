namespace Podelka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeliveryMethods",
                c => new
                    {
                        DeliveryMethodId = c.Byte(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DeliveryMethodId);
            
            CreateTable(
                "dbo.WorkroomDeliveryMethods",
                c => new
                    {
                        WorkroomId = c.Long(nullable: false),
                        DeliveryMethodId = c.Byte(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => new { t.WorkroomId, t.DeliveryMethodId })
                .ForeignKey("dbo.DeliveryMethods", t => t.DeliveryMethodId, cascadeDelete: true)
                .ForeignKey("dbo.Workrooms", t => t.WorkroomId, cascadeDelete: true)
                .Index(t => t.WorkroomId)
                .Index(t => t.DeliveryMethodId);
            
            CreateTable(
                "dbo.Workrooms",
                c => new
                    {
                        WorkroomId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        RegisterTypeId = c.Byte(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        CountGood = c.Short(nullable: false),
                        CountMedium = c.Short(nullable: false),
                        CountBad = c.Short(nullable: false),
                        DateRegistration = c.DateTime(nullable: false),
                        WorkroomRegisterTypes_WorkroomRegisterTypeId = c.Byte(),
                    })
                .PrimaryKey(t => t.WorkroomId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.WorkroomRegisterTypes", t => t.WorkroomRegisterTypes_WorkroomRegisterTypeId)
                .Index(t => t.UserId)
                .Index(t => t.WorkroomRegisterTypes_WorkroomRegisterTypeId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Long(nullable: false, identity: true),
                        WorkroomId = c.Long(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Single(nullable: false),
                        StatusReady = c.String(),
                        Material = c.String(),
                        Size = c.String(),
                        Weight = c.String(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Workrooms", t => t.WorkroomId, cascadeDelete: true)
                .Index(t => t.WorkroomId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(),
                        SecondName = c.String(),
                        City = c.String(),
                        Phone = c.String(),
                        Skype = c.String(),
                        SocialNetwork = c.String(),
                        PersonalWebsite = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.WorkroomPayMethods",
                c => new
                    {
                        WorkroomId = c.Long(nullable: false),
                        PayMethodId = c.Byte(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => new { t.WorkroomId, t.PayMethodId })
                .ForeignKey("dbo.PayMethods", t => t.PayMethodId, cascadeDelete: true)
                .ForeignKey("dbo.Workrooms", t => t.WorkroomId, cascadeDelete: true)
                .Index(t => t.WorkroomId)
                .Index(t => t.PayMethodId);
            
            CreateTable(
                "dbo.PayMethods",
                c => new
                    {
                        PayMethodId = c.Byte(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.PayMethodId);
            
            CreateTable(
                "dbo.WorkroomRegisterTypes",
                c => new
                    {
                        WorkroomRegisterTypeId = c.Byte(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.WorkroomRegisterTypeId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Workrooms", "WorkroomRegisterTypes_WorkroomRegisterTypeId", "dbo.WorkroomRegisterTypes");
            DropForeignKey("dbo.WorkroomPayMethods", "WorkroomId", "dbo.Workrooms");
            DropForeignKey("dbo.WorkroomPayMethods", "PayMethodId", "dbo.PayMethods");
            DropForeignKey("dbo.WorkroomDeliveryMethods", "WorkroomId", "dbo.Workrooms");
            DropForeignKey("dbo.Workrooms", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Products", "WorkroomId", "dbo.Workrooms");
            DropForeignKey("dbo.WorkroomDeliveryMethods", "DeliveryMethodId", "dbo.DeliveryMethods");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.WorkroomPayMethods", new[] { "PayMethodId" });
            DropIndex("dbo.WorkroomPayMethods", new[] { "WorkroomId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Products", new[] { "WorkroomId" });
            DropIndex("dbo.Workrooms", new[] { "WorkroomRegisterTypes_WorkroomRegisterTypeId" });
            DropIndex("dbo.Workrooms", new[] { "UserId" });
            DropIndex("dbo.WorkroomDeliveryMethods", new[] { "DeliveryMethodId" });
            DropIndex("dbo.WorkroomDeliveryMethods", new[] { "WorkroomId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.WorkroomRegisterTypes");
            DropTable("dbo.PayMethods");
            DropTable("dbo.WorkroomPayMethods");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Products");
            DropTable("dbo.Workrooms");
            DropTable("dbo.WorkroomDeliveryMethods");
            DropTable("dbo.DeliveryMethods");
        }
    }
}
