namespace FurnitureStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateUserRoles : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO UserRoles (Id, RoleType) VALUES (1, 'Korisnik')");
            Sql("INSERT INTO UserRoles (Id, RoleType) VALUES (2, 'Administrator')");
        }
        
        public override void Down()
        {
        }
    }
}
