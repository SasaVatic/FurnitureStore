﻿using System.Data.Entity;

namespace FurnitureStore.Models
{
    public class FurnitureStoreDbContext : DbContext
    {
        public FurnitureStoreDbContext() : base("FurnitureStoreConnection") { }
        public DbSet<User> tblUsers { get; set; }
        public DbSet<UserRole> tblUserRoles { get; set; }
        public DbSet<Address> tblStreetAddresses { get; set; }
        public DbSet<Shop> tblShops { get; set; }
        public DbSet<ProductType> tblProductTypes { get; set; }
        public DbSet<Product> tblProducts { get; set; }
        public DbSet<PurchasedProduct> tblPurchasedProducts { get; set; }
        public DbSet<Bill> tblBills { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>()
        //        .HasOptional(u => u.Address)
        //        .WithRequired();

        //    modelBuilder.Entity<Shop>()
        //        .HasOptional(s => s.Address)
        //        .WithRequired();

        //    modelBuilder.Entity<PurchasedProduct>()
        //        .HasOptional(p => p.Bill)
        //        .WithRequired();
        //}
    }
}