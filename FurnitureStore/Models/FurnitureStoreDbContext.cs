using System.Data.Entity;
using System.Diagnostics;

namespace FurnitureStore.Models
{
    public class FurnitureStoreDbContext : DbContext
    {
        public FurnitureStoreDbContext() : base("FurnitureEntities") { Database.Log = sql => Debug.Write(sql); }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Address> StreetAddresses { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<UploadImage> UploadImages { get; set; }
    }
}