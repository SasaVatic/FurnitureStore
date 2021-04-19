using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FurnitureStore.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        #region Properties
        /// <summary>
        /// Ime korisnika
        /// </summary>
        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(50, ErrorMessage = "Maksimalna dužina Imena je 50 karaktera")]
        [Display(Name = "Ime")]
        public string Name { get; set; }

        /// <summary>
        /// Prezime korisnika
        /// </summary>
        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(50, ErrorMessage = "Maksimalna dužina Prezimena je 50 karaktera")]
        [Display(Name = "Prezime")]
        public string Surname { get; set; }
        #endregion

        #region Navigation Properties

        /// <summary>
        /// Predstavlja adresu na kojoj korisnik stanuje
        /// </summary>
        public virtual ICollection<Address> Addresses { get; set; }

        /// <summary>
        /// Predstavlja sve racune korisnika za kupljene proizvode
        /// </summary>
        public virtual ICollection<Bill> Bills { get; set; }
        #endregion
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class FurnitureStoreDbContext : IdentityDbContext<User>
    {
        public FurnitureStoreDbContext()
            : base("FurnitureConnection", throwIfV1Schema: false)
        {
        }

        public static FurnitureStoreDbContext Create()
        
        {
            return new FurnitureStoreDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");

            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");

            modelBuilder.Entity<IdentityRole>().ToTable("Roles");

            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<ShoppingCartProduct> ShoppingCartProducts { get; set; }
        public DbSet<UploadImage> UploadImages { get; set; }
    }
}