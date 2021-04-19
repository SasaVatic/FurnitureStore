namespace FurnitureStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetUniqueConstraints : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Shops", "OwnerName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Products", "ProductKey", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Products", "ProductName", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Shops", "PIB", unique: true);
            CreateIndex("dbo.Shops", "BZR", unique: true);
            CreateIndex("dbo.Products", "ProductKey", unique: true);
            CreateIndex("dbo.ProductTypes", "TypeName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProductTypes", new[] { "TypeName" });
            DropIndex("dbo.Products", new[] { "ProductKey" });
            DropIndex("dbo.Shops", new[] { "BZR" });
            DropIndex("dbo.Shops", new[] { "PIB" });
            AlterColumn("dbo.Products", "ProductName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Products", "ProductKey", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Shops", "OwnerName", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
