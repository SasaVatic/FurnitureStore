namespace FurnitureStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotNullAndLengthConstraints : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PurchasedProducts", "ShopName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Surname", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Addresses", "StreetName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Addresses", "StreetNumber", c => c.String(nullable: false, maxLength: 4));
            AlterColumn("dbo.Addresses", "City", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Shops", "ShopName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Shops", "OwnerName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Shops", "PhoneNumber", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Shops", "Email", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Shops", "WebPageURL", c => c.String(nullable: false, maxLength: 253));
            AlterColumn("dbo.Shops", "BZR", c => c.String(nullable: false, maxLength: 11));
            AlterColumn("dbo.Products", "ProductKey", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Products", "ProductName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Products", "MadeIn", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.ProductTypes", "TypeName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ProductTypes", "Description", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.UserRoles", "RoleType", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserRoles", "RoleType", c => c.String());
            AlterColumn("dbo.ProductTypes", "Description", c => c.String());
            AlterColumn("dbo.ProductTypes", "TypeName", c => c.String());
            AlterColumn("dbo.Products", "MadeIn", c => c.String());
            AlterColumn("dbo.Products", "ProductName", c => c.String());
            AlterColumn("dbo.Products", "ProductKey", c => c.Int(nullable: false));
            AlterColumn("dbo.Shops", "BZR", c => c.String());
            AlterColumn("dbo.Shops", "WebPageURL", c => c.String());
            AlterColumn("dbo.Shops", "Email", c => c.String());
            AlterColumn("dbo.Shops", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Shops", "OwnerName", c => c.String());
            AlterColumn("dbo.Shops", "ShopName", c => c.String());
            AlterColumn("dbo.Addresses", "City", c => c.String());
            AlterColumn("dbo.Addresses", "StreetNumber", c => c.String());
            AlterColumn("dbo.Addresses", "StreetName", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "Surname", c => c.String());
            AlterColumn("dbo.Users", "Name", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Username", c => c.String());
            AlterColumn("dbo.PurchasedProducts", "ShopName", c => c.String());
        }
    }
}
