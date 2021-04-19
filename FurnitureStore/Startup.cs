using FurnitureStore.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FurnitureStore.Startup))]
namespace FurnitureStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }
        private void CreateRolesAndUsers()
        {
            FurnitureStoreDbContext _context = new FurnitureStoreDbContext();

            // RoleManager klasa u Identity Frameworku je zaduzena za upravljanje ulogama
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));

            // UserManager klasa u Identity Frameworku je zaduzena za upravljanje korisnicima
            var userManager = new UserManager<User>(new UserStore<User>(_context));

            // Dodavanje admina prilikom prvog pokretanja aplikacije
            if (!roleManager.RoleExists(RoleName.Admin))
            {
                var role = new IdentityRole();
                role.Name = RoleName.Admin;

                roleManager.Create(role);

                var user = new User();
                user.UserName = "Mara";
                user.Email = "mara@gmail.com";
                user.Name = "Marija";
                user.Surname = "Marić";
                string userPassword = "Admin1234";

                // Pravi novog korisnika i upisuje ga u bazu i vraca istancu klase IdentityResult
                var checkUser = userManager.Create(user, userPassword);

                // IdentityResult klasa u sebi sadrzi properti Succeeded tipa bool
                // koja ce biti true ako je korisnik uspesno dodat u bazu
                if (checkUser.Succeeded)
                {
                    // Posto je korisnik uspesno dodat u bazu dodeli mu ulogu Admina
                    var addUserToRole = userManager.AddToRole(user.Id, RoleName.Admin);
                }
            }
            // Dodavanje uloge za obicnog korisnika(kupca)
            else if (!roleManager.RoleExists(RoleName.Customer))
            {
                var role = new IdentityRole();
                role.Name = RoleName.Customer;
                roleManager.Create(role);
            }
        }
    }
}
