namespace FurnitureStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeBZRColumnLengthConstraint : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Shops", "BZR", c => c.String(nullable: false, maxLength: 13));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Shops", "BZR", c => c.String(nullable: false, maxLength: 11));
        }
    }
}
