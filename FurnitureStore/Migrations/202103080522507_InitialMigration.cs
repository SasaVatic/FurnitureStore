namespace FurnitureStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tax = c.Int(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchaseDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PurchasedProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        ShopName = c.String(),
                        BillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bills", t => t.BillId, cascadeDelete: true)
                .Index(t => t.BillId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        Email = c.String(),
                        UserRoleId = c.Byte(nullable: false),
                        AddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.UserRoles", t => t.UserRoleId, cascadeDelete: true)
                .Index(t => t.UserRoleId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StreetName = c.String(),
                        StreetNumber = c.String(),
                        ZipCode = c.Int(nullable: false),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShopName = c.String(),
                        OwnerName = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        WebPageURL = c.String(),
                        PIB = c.Int(nullable: false),
                        BZR = c.String(),
                        AddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductKey = c.Int(nullable: false),
                        ProductName = c.String(),
                        MadeIn = c.String(),
                        ProductionDate = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        Picture = c.Binary(),
                        ShopId = c.Int(nullable: false),
                        ProductTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductTypes", t => t.ProductTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Shops", t => t.ShopId, cascadeDelete: true)
                .Index(t => t.ShopId)
                .Index(t => t.ProductTypeId);
            
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        RoleType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserRoleId", "dbo.UserRoles");
            DropForeignKey("dbo.Bills", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Products", "ShopId", "dbo.Shops");
            DropForeignKey("dbo.Products", "ProductTypeId", "dbo.ProductTypes");
            DropForeignKey("dbo.Shops", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.PurchasedProducts", "BillId", "dbo.Bills");
            DropIndex("dbo.Products", new[] { "ProductTypeId" });
            DropIndex("dbo.Products", new[] { "ShopId" });
            DropIndex("dbo.Shops", new[] { "AddressId" });
            DropIndex("dbo.Users", new[] { "AddressId" });
            DropIndex("dbo.Users", new[] { "UserRoleId" });
            DropIndex("dbo.PurchasedProducts", new[] { "BillId" });
            DropIndex("dbo.Bills", new[] { "UserId" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.ProductTypes");
            DropTable("dbo.Products");
            DropTable("dbo.Shops");
            DropTable("dbo.Addresses");
            DropTable("dbo.Users");
            DropTable("dbo.PurchasedProducts");
            DropTable("dbo.Bills");
        }
    }
}
