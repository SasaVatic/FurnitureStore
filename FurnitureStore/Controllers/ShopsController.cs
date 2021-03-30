using FurnitureStore.Models;
using FurnitureStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureStore.Controllers
{
    public class ShopsController : Controller
    {
        private readonly FurnitureStoreDbContext _context;
        public ShopsController()
        {
            _context = new FurnitureStoreDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Shops
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetShops()
        {
            var shops = _context.Shops.ToList();
            var addresses = _context.StreetAddresses.ToList();

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

        [HttpGet]
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
                var addressDb = _context.StreetAddresses.SingleOrDefault(x => x.ShopId == shopDb.Id);

                var existingShop = new ShopFormViewModel(shopDb)
                {
                    Address = new AddressViewModel(addressDb)
                };

                return View("ShopForm", existingShop);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEdit(ShopFormViewModel shopFormVM)
        {
            if (shopFormVM.Id == 0)
            {
                var newShop = new Shop
                {
                    Id = shopFormVM.Id,
                    ShopName = shopFormVM.ShopName,
                    OwnerName = shopFormVM.OwnerName,
                    PhoneNumber = shopFormVM.PhoneNumber,
                    Email = shopFormVM.Email,
                    WebPageURL = shopFormVM.WebPageURL,
                    BZR = shopFormVM.BZR,
                    PIB = shopFormVM.PIB
                };

                _context.Shops.Add(newShop);

                var newAddress = new Address
                {
                    Id = shopFormVM.Id,
                    StreetName = shopFormVM.Address.StreetName,
                    StreetNumber = shopFormVM.Address.StreetNumber,
                    ZipCode = shopFormVM.Address.ZipCode,
                    City = shopFormVM.Address.City,
                    ShopId = shopFormVM.Id
                };

                _context.StreetAddresses.Add(newAddress);

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
                shopDb.PIB = shopFormVM.PIB;

                var addressDb = _context.StreetAddresses.SingleOrDefault(x => x.ShopId == shopDb.Id);

                addressDb.StreetName = shopFormVM.Address.StreetName;
                addressDb.StreetNumber = shopFormVM.Address.StreetNumber;
                addressDb.ZipCode = shopFormVM.Address.ZipCode;
                addressDb.City = shopFormVM.Address.City;
                addressDb.ShopId = shopFormVM.Id;

                _context.SaveChanges();

                return Json(new { success = true, message = "Uspešno izmenjeno" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Delete(int id)
        {
            var shopDb = _context.Shops.FirstOrDefault(x => x.Id == id);
            _context.Shops.Remove(shopDb);

            _context.SaveChanges();

            return Json(new { success = true, message = "Uspešno obrisano" }, JsonRequestBehavior.AllowGet);
        }
    }
}