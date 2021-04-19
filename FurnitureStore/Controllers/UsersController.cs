using FurnitureStore.Models;
using FurnitureStore.ViewModels;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace FurnitureStore.Controllers
{
    // Da bi zabranili ulogovanom korisniku koji nije admin
    // pristup stranici preko url-a
    [Authorize(Roles = RoleName.Admin)]
    public class UsersController : Controller
    {
        private readonly FurnitureStoreDbContext _context;
        public UsersController()
        {
            _context = new FurnitureStoreDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetUsers()
        {
            var users = _context.Users.Include(u => u.Roles).ToList();
            var adresses = _context.Addresses.ToList();

            var usersQuery = from u in users
                             from a in adresses
                             where u.Id == a.UserId
                             select new
                             {
                                 Id = u.Id,
                                 Username = u.UserName,
                                 Name = u.Name,
                                 Surname = u.Surname,
                                 Email = u.Email,
                                 Address = a.StreetName + " " + a.StreetNumber + " " + a.City,
                                 Role = (from rol in _context.Roles
                                         from usr in rol.Users
                                         where usr.UserId == u.Id
                                         select rol).First().Name
                                 // Iz navigacijskog propertija ICollection<IdentityUserRoles>
                                 // iz tabele IdentityRoles proveri da li se u toj tabeli
                                 // nalazi kljuc trenutno selektovanog korisnika, ako se kljuc
                                 // nalazi u navigacijskom propertiju selektuj ime uloge(Name)
                             };


            return Json(new { data = usersQuery }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddOrEdit(string id)
        {
            if (id == null)
            {
                var userRolesDb = _context.Roles.ToList();
                var userRoles = new List<UserRoleViewModel>();

                foreach (var role in userRolesDb)
                {
                    userRoles.Add(new UserRoleViewModel(role.Id, role.Name));
                }

                var newUser = new UserFormViewModel()
                {
                    UserRoles = userRoles,
                    Address = new AddressViewModel()
                };

                return View("UserForm", newUser);
            }
            else
            {
                var userDb = _context.Users.FirstOrDefault(x => x.Id == id);
                var addressDb = _context.Addresses.FirstOrDefault(x => x.UserId == userDb.Id);

                var userRolesDb = _context.Roles.ToList();

                var userRoles = new List<UserRoleViewModel>();

                foreach (var role in userRolesDb)
                {
                    userRoles.Add(new UserRoleViewModel(role.Id, role.Name));
                }

                var existingUser = new UserFormViewModel()
                {
                    Id = userDb.Id,
                    Username = userDb.UserName,
                    Name = userDb.Name,
                    Surname = userDb.Surname,
                    Email = userDb.Email,
                    UserRoleId = (from rol in _context.Roles
                                  from usr in rol.Users
                                  where usr.UserId == userDb.Id
                                  select rol).First().Id,
                    UserRoles = userRoles,
                    Address = new AddressViewModel(addressDb)
                };

                return View("EditUserForm", existingUser);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEdit(UserFormViewModel userFormVM)
        {
            if (userFormVM.Id == null)
            {
                var newUser = new User
                {
                    UserName = userFormVM.Username,
                    Name = userFormVM.Name,
                    Surname = userFormVM.Surname,
                    Email = userFormVM.Email
                };

                // Dodaj novog korisnika
                var userManager = new UserManager<User>(new UserStore<User>(_context));
                var result = userManager.Create(newUser, userFormVM.Password);

                // Ako je korisnik uspesno dodat
                if (result.Succeeded)
                {
                    // Dodeli korisniku izabranu ulogu
                    userManager.AddToRole(newUser.Id, _context.Roles.SingleOrDefault(x => x.Id == userFormVM.UserRoleId).Name);

                    var newAddress = new Address
                    {
                        StreetName = userFormVM.Address.StreetName,
                        StreetNumber = userFormVM.Address.StreetNumber,
                        ZipCode = (int)userFormVM.Address.ZipCode,
                        City = userFormVM.Address.City,
                        UserId = newUser.Id
                    };

                    _context.Addresses.Add(newAddress);

                    var userRole = new IdentityUserRole
                    {
                        UserId = newUser.Id,
                        RoleId = userFormVM.UserRoleId
                    };

                    _context.SaveChanges();
                }              

                return Json(new { success = true, message = "Uspešno sačuvano" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var userDb = _context.Users.SingleOrDefault(x => x.Id == userFormVM.Id);

                userDb.Name = userFormVM.Name;
                userDb.Surname = userFormVM.Surname;
                userDb.Email = userFormVM.Email;

                var addressDb = _context.Addresses.SingleOrDefault(x => x.UserId == userDb.Id);

                addressDb.StreetName = userFormVM.Address.StreetName;
                addressDb.StreetNumber = userFormVM.Address.StreetNumber;
                addressDb.ZipCode = (int)userFormVM.Address.ZipCode;
                addressDb.City = userFormVM.Address.City;

                // Dodavanje nove uloge korisniku
                var userManager = new UserManager<User>(new UserStore<User>(_context));

                // Posto ne znamo da li je korisniku promenjena uloga prvo moramo da
                // dobavimo iz baze njegovu trenutnu ulogu
                var oldRole = (from rol in _context.Roles
                               from usr in rol.Users
                               where usr.UserId == userDb.Id
                               select rol).First();

                // Ukloni korisnika sa trenutne uloge
                userManager.RemoveFromRole(userDb.Id, oldRole.Name);
                // Dodeli mu novu ulogu
                userManager.AddToRole(userDb.Id, _context.Roles.Single(x => x.Id == userFormVM.UserRoleId).Name);

                _context.SaveChanges();

                return Json(new { success = true, message = "Uspešno izmenjeno" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            var userManager = new UserManager<User>(new UserStore<User>(_context));
            var userDb = _context.Users.FirstOrDefault(x => x.Id == id);

            var billDb = _context.Bills.FirstOrDefault(x => x.UserId == userDb.Id);

            // Proveri da li korisnik ima racuna, ako nema izbrisi samo korisnika
            // jer ako je racun null bacice exception
            if (billDb != null)
            {
                _context.Bills.Remove(billDb);
            }

            _context.SaveChanges();

            userManager.Delete(userDb);

            return Json(new { success = true, message = "Uspešno obrisano", JsonRequestBehavior.AllowGet });
        }
    }
}