namespace Podelka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Short(nullable: false, identity: true),
                        SectionId = c.Byte(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Sections", t => t.SectionId, cascadeDelete: true)
                .Index(t => t.SectionId);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        SectionId = c.Byte(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SectionId);
            
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
                        WorkroomRegisterTypeId = c.Byte(nullable: false),
                        SectionId = c.Byte(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        CountGood = c.Short(nullable: false),
                        CountMedium = c.Short(nullable: false),
                        CountBad = c.Short(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.WorkroomId)
                .ForeignKey("dbo.RegisterTypeWorkrooms", t => t.WorkroomRegisterTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Sections", t => t.SectionId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.WorkroomRegisterTypeId)
                .Index(t => t.SectionId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Long(nullable: false, identity: true),
                        WorkroomId = c.Long(nullable: false),
                        CategoryId = c.Short(nullable: false),
                        ProductGenderTypeId = c.Byte(nullable: false),
                        ProductStatusReadyId = c.Byte(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Single(nullable: false),
                        PriceDiscount = c.Single(nullable: false),
                        Size = c.String(),
                        Weight = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.GenderTypeProducts", t => t.ProductGenderTypeId, cascadeDelete: true)
                .ForeignKey("dbo.StatusReadyProducts", t => t.ProductStatusReadyId, cascadeDelete: true)
                .ForeignKey("dbo.Workrooms", t => t.WorkroomId, cascadeDelete: true)
                .Index(t => t.WorkroomId)
                .Index(t => t.ProductGenderTypeId)
                .Index(t => t.ProductStatusReadyId);
            
            CreateTable(
                "dbo.GenderTypeProducts",
                c => new
                    {
                        ProductGenderTypeId = c.Byte(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ProductGenderTypeId);
            
            CreateTable(
                "dbo.ProductMaterials",
                c => new
                    {
                        ProductId = c.Long(nullable: false),
                        MaterialId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.MaterialId })
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.MaterialId);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        MaterialId = c.Byte(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.MaterialId);
            
            CreateTable(
                "dbo.StatusReadyProducts",
                c => new
                    {
                        ProductStatusReadyId = c.Byte(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ProductStatusReadyId);
            
            CreateTable(
                "dbo.RegisterTypeWorkrooms",
                c => new
                    {
                        WorkroomRegisterTypeId = c.Byte(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.WorkroomRegisterTypeId);
            
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
                        DateRegistration = c.DateTime(nullable: false),
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
                "dbo.Phones",
                c => new
                    {
                        PhoneId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        PhoneTypeId = c.Byte(nullable: false),
                        Number = c.String(),
                    })
                .PrimaryKey(t => t.PhoneId)
                .ForeignKey("dbo.TypePhones", t => t.PhoneTypeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PhoneTypeId);
            
            CreateTable(
                "dbo.TypePhones",
                c => new
                    {
                        PhoneTypeId = c.Byte(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.PhoneTypeId);
            
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
            DropForeignKey("dbo.WorkroomPayMethods", "WorkroomId", "dbo.Workrooms");
            DropForeignKey("dbo.WorkroomPayMethods", "PayMethodId", "dbo.PayMethods");
            DropForeignKey("dbo.WorkroomDeliveryMethods", "WorkroomId", "dbo.Workrooms");
            DropForeignKey("dbo.Workrooms", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Phones", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Phones", "PhoneTypeId", "dbo.TypePhones");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Workrooms", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Workrooms", "WorkroomRegisterTypeId", "dbo.RegisterTypeWorkrooms");
            DropForeignKey("dbo.Products", "WorkroomId", "dbo.Workrooms");
            DropForeignKey("dbo.Products", "ProductStatusReadyId", "dbo.StatusReadyProducts");
            DropForeignKey("dbo.ProductMaterials", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductMaterials", "MaterialId", "dbo.Materials");
            DropForeignKey("dbo.Products", "ProductGenderTypeId", "dbo.GenderTypeProducts");
            DropForeignKey("dbo.WorkroomDeliveryMethods", "DeliveryMethodId", "dbo.DeliveryMethods");
            DropForeignKey("dbo.Categories", "SectionId", "dbo.Sections");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.WorkroomPayMethods", new[] { "PayMethodId" });
            DropIndex("dbo.WorkroomPayMethods", new[] { "WorkroomId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Phones", new[] { "PhoneTypeId" });
            DropIndex("dbo.Phones", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ProductMaterials", new[] { "MaterialId" });
            DropIndex("dbo.ProductMaterials", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "ProductStatusReadyId" });
            DropIndex("dbo.Products", new[] { "ProductGenderTypeId" });
            DropIndex("dbo.Products", new[] { "WorkroomId" });
            DropIndex("dbo.Workrooms", new[] { "SectionId" });
            DropIndex("dbo.Workrooms", new[] { "WorkroomRegisterTypeId" });
            DropIndex("dbo.Workrooms", new[] { "UserId" });
            DropIndex("dbo.WorkroomDeliveryMethods", new[] { "DeliveryMethodId" });
            DropIndex("dbo.WorkroomDeliveryMethods", new[] { "WorkroomId" });
            DropIndex("dbo.Categories", new[] { "SectionId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PayMethods");
            DropTable("dbo.WorkroomPayMethods");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.TypePhones");
            DropTable("dbo.Phones");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.RegisterTypeWorkrooms");
            DropTable("dbo.StatusReadyProducts");
            DropTable("dbo.Materials");
            DropTable("dbo.ProductMaterials");
            DropTable("dbo.GenderTypeProducts");
            DropTable("dbo.Products");
            DropTable("dbo.Workrooms");
            DropTable("dbo.WorkroomDeliveryMethods");
            DropTable("dbo.DeliveryMethods");
            DropTable("dbo.Sections");
            DropTable("dbo.Categories");
        }
    }
}
