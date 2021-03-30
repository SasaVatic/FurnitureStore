using FurnitureStore.Models;
using FurnitureStore.ViewModels;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace FurnitureStore.Controllers
{
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
            var users = _context.Users.Include(u => u.UserRole).ToList();
            var adresses = _context.StreetAddresses.ToList();

            var usersQuery = from u in users
                             from a in adresses
                             where u.Id == a.UserId
                             select new
                             {
                                 Id = u.Id,
                                 Username = u.Username,
                                 Password = u.Password,
                                 Name = u.Name,
                                 Surname = u.Surname,
                                 Email = u.Email,
                                 Address = a.StreetName + " " + a.StreetNumber + " " + a.City,
                                 UserRole = u.UserRole.RoleType
                             };


            return Json(new { data = usersQuery }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                var userRolesDb = _context.UserRoles.ToList();
                var userRoles = new List<UserRoleViewModel>();

                foreach (var role in userRolesDb)
                {
                    userRoles.Add(new UserRoleViewModel(role.Id, role.RoleType));
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
                var addressDb = _context.StreetAddresses.FirstOrDefault(x => x.UserId == userDb.Id);

                var userRolesDb = _context.UserRoles.ToList();
                var userRoles = new List<UserRoleViewModel>();

                foreach (var role in userRolesDb)
                {
                    userRoles.Add(new UserRoleViewModel(role.Id, role.RoleType));
                }

                var existingUser = new UserFormViewModel(userDb)
                {
                    UserRoles = userRoles,
                    Address = new AddressViewModel(addressDb)
                };

                return View("UserForm", existingUser);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEdit(UserFormViewModel userFormVM)
        {
            if (userFormVM.Id == 0)
            {
                var newUser = new User
                {
                    Id = userFormVM.Id,
                    Username = userFormVM.Username,
                    Password = userFormVM.Password,
                    Name = userFormVM.Name,
                    Surname = userFormVM.Surname,
                    Email = userFormVM.Email,
                    UserRoleId = userFormVM.UserRoleId
                };

                _context.Users.Add(newUser);

                var newAddress = new Address
                {
                    StreetName = userFormVM.Address.StreetName,
                    StreetNumber = userFormVM.Address.StreetNumber,
                    ZipCode = userFormVM.Address.ZipCode,
                    City = userFormVM.Address.City,
                    UserId = newUser.Id
                };

                _context.StreetAddresses.Add(newAddress);

                _context.SaveChanges();

                return Json(new { success = true, message = "Uspešno sačuvano" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var userDb = _context.Users.SingleOrDefault(x => x.Id == userFormVM.Id);

                userDb.Username = userFormVM.Username;
                userDb.Password = userFormVM.Password;
                userDb.Name = userFormVM.Name;
                userDb.Surname = userFormVM.Surname;
                userDb.Email = userFormVM.Email;
                userDb.UserRoleId = userFormVM.UserRoleId;

                var addressDb = _context.StreetAddresses.SingleOrDefault(x => x.UserId == userDb.Id);

                addressDb.StreetName = userFormVM.Address.StreetName;
                addressDb.StreetNumber = userFormVM.Address.StreetNumber;
                addressDb.ZipCode = userFormVM.Address.ZipCode;
                addressDb.City = userFormVM.Address.City;

                _context.SaveChanges();

                return Json(new { success = true, message = "Uspešno izmenjeno" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var userDb = _context.Users.FirstOrDefault(x => x.Id == id);
            _context.Users.Remove(userDb);

            _context.SaveChanges();

            return Json(new { success = true, message = "Uspešno obrisano", JsonRequestBehavior.AllowGet });
        }
    }
}