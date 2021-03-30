namespace FurnitureStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnableCascadeDeleteForAddressTable : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER TABLE [dbo].[Addresses] DROP CONSTRAINT [FK_dbo.Addresses_dbo.Shops_ShopId]");
            Sql("ALTER TABLE [dbo].[Addresses] DROP CONSTRAINT [FK_dbo.Addresses_dbo.Users_UserId]");
            Sql("ALTER TABLE [dbo].[Addresses] WITH CHECK ADD CONSTRAINT [FK_dbo.Addresses_dbo.Shops_ShopId] FOREIGN KEY ([ShopId]) REFERENCES [dbo].[Shops] ([Id]) ON DELETE CASCADE");
            Sql("ALTER TABLE [dbo].[Addresses] WITH CHECK ADD CONSTRAINT [FK_dbo.Addresses_dbo.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE");
        }
        
        public override void Down()
        {
        }
    }
}
