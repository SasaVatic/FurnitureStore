namespace FurnitureStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUploadImageTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UploadImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OldName = c.String(maxLength: 550),
                        NewName = c.String(maxLength: 550),
                        ImagePath = c.String(maxLength: 500),
                        ImageBytes = c.Binary(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UploadImages", "ProductId", "dbo.Products");
            DropIndex("dbo.UploadImages", new[] { "ProductId" });
            DropTable("dbo.UploadImages");
        }
    }
}
