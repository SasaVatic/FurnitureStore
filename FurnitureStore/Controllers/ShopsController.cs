using FurnitureStore.Models;
using FurnitureStore.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FurnitureStore.Controllers
{
    /// <summary>
    /// Kontroler za salone namestaja
    /// </summary>
    public class ShopsController : Controller
    {
        #region Konekcija ka bazi

        private readonly FurnitureStoreDbContext _context;
        public ShopsController()
        {
            _context = new FurnitureStoreDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion

        // GET: /Shops
        #region Metoda za dobavljanje Index view-a za salone namestaja
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.Admin))
            {
                return View();
            }
            return View("CustomerShopView");
        }
        #endregion

        // GET: /Shops/GetShops
        #region Metoda za dobavljanje podataka za tabelu salona namestaja
        [HttpGet]
        public ActionResult GetShops()
        {
            var shops = _context.Shops.ToList();
            var addresses = _context.Addresses.ToList();

            var shopsQuery = from s in shops
                             from a in addresses
                             where s.Id == a.ShopId
                             select new
                             {
                                 Id = s.Id,
                                 ShopName = s.ShopName,
                                 Address = a.StreetName + " " + a.StreetNumber + " " + a.City,
                                 OwnerName = s.OwnerName,
                                 PhoneNumber = s.PhoneNumber,
                                 Email = s.Email,
                                 WebPageURL = s.WebPageURL,
                                 PIB = s.PIB,
                                 BZR = s.BZR
                             };

            return Json(new { data = shopsQuery }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        // GET: /Shops/AddOrEdit/1
        #region Metoda za dobavljanje ShopForm view-a za dodavanje ili izmenu salona namestaja

        [HttpGet]
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                var shop = new ShopFormViewModel
                {
                    Address = new AddressViewModel()
                };

                return View("ShopForm", shop);
            }
            else
            {
                var shopDb = _context.Shops.SingleOrDefault(x => x.Id == id);
                var addressDb = _context.Addresses.SingleOrDefault(x => x.ShopId == shopDb.Id);

                var existingShop = new ShopFormViewModel(shopDb)
                {
                    Address = new AddressViewModel(addressDb)
                };

                return View("ShopForm", existingShop);
            }
        }
        #endregion

        // POST: /Shops/AddOrEdit
        #region Metoda za dodavanje ili izmenu salona u bazi

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEdit(ShopFormViewModel shopFormVM)
        {
            if (shopFormVM.Id == 0)
            {
                if (!ModelState.IsValid)
                {
                    // Za validaciju - ako prilikom submita nisu prosledjena validna unique polja ModelState nece biti validan
                    // i vratice na view json sa odgovarajucim podacima
                    if (!ModelState.IsValidField("BZR") && !ModelState.IsValidField("PIB"))
                    {
                        return Json(new { success = false, prop = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else if (!ModelState.IsValidField("BZR"))
                    {
                        return Json(new { success = false, prop = "bzr" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, prop = "pib" }, JsonRequestBehavior.AllowGet);
                    }
                }

                var newShop = new Shop
                {
                    Id = shopFormVM.Id,
                    ShopName = shopFormVM.ShopName,
                    OwnerName = shopFormVM.OwnerName,
                    PhoneNumber = shopFormVM.PhoneNumber,
                    Email = shopFormVM.Email,
                    WebPageURL = shopFormVM.WebPageURL,
                    BZR = shopFormVM.BZR,
                    PIB = (int)shopFormVM.PIB
                };

                _context.Shops.Add(newShop);

                var newAddress = new Address
                {
                    Id = shopFormVM.Id,
                    StreetName = shopFormVM.Address.StreetName,
                    StreetNumber = shopFormVM.Address.StreetNumber,
                    ZipCode = (int)shopFormVM.Address.ZipCode,
                    City = shopFormVM.Address.City,
                    ShopId = shopFormVM.Id
                };

                _context.Addresses.Add(newAddress);

                _context.SaveChanges();

                return Json(new { success = true, message = "Uspešno sačuvano" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var shopDb = _context.Shops.SingleOrDefault(x => x.Id == shopFormVM.Id);

                shopDb.ShopName = shopFormVM.ShopName;
                shopDb.OwnerName = shopFormVM.OwnerName;
                shopDb.PhoneNumber = shopFormVM.PhoneNumber;
                shopDb.Email = shopFormVM.Email;
                shopDb.WebPageURL = shopFormVM.WebPageURL;
                shopDb.BZR = shopFormVM.BZR;
                shopDb.PIB = (int)shopFormVM.PIB;

                var addressDb = _context.Addresses.SingleOrDefault(x => x.ShopId == shopDb.Id);

                addressDb.StreetName = shopFormVM.Address.StreetName;
                addressDb.StreetNumber = shopFormVM.Address.StreetNumber;
                addressDb.ZipCode = (int)shopFormVM.Address.ZipCode;
                addressDb.City = shopFormVM.Address.City;
                addressDb.ShopId = shopFormVM.Id;

                _context.SaveChanges();

                return Json(new { success = true, message = "Uspešno izmenjeno" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        // POST: /Shops/Delete/1
        #region Metoda za brisanje salona namestaja

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var shopDb = _context.Shops.FirstOrDefault(x => x.Id == id);
            _context.Shops.Remove(shopDb);

            _context.SaveChanges();

            return Json(new { success = true, message = "Uspešno obrisano" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}