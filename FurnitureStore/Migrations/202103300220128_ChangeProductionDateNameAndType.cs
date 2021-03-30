namespace FurnitureStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeProductionDateNameAndType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProductionYear", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "ProductionDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ProductionDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Products", "ProductionYear");
        }
    }
}
