namespace FurnitureStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniqueConstraints : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER TABLE Users ADD UNIQUE (Username)");
            Sql("ALTER TABLE Shops ADD UNIQUE (PIB)");
            Sql("ALTER TABLE Shops ADD UNIQUE (BZR)");
            Sql("ALTER TABLE ProductTypes ADD UNIQUE (TypeName)");
            Sql("ALTER TABLE Products ADD UNIQUE (ProductKey)");
        }
        
        public override void Down()
        {
        }
    }
}
