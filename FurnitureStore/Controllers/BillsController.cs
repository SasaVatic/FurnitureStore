using FurnitureStore.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using FurnitureStore.ViewModels;

namespace FurnitureStore.Controllers
{
    public class BillsController : Controller
    {
        #region Konekcija ka bazi
        private readonly FurnitureStoreDbContext _context;
        public BillsController()
        {
            _context = new FurnitureStoreDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion

        // GET: Bills
        [HttpGet]
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult GetBills()
        {
            var query = from b in _context.Bills.Include(x => x.ShoppingCartProducts)
                        select new
                        {
                            Customer = b.User.Name + " " + b.User.Surname,
                            PurchaseDate = b.PurchaseDate,
                            TotalPrice = b.TotalPrice,
                            PurchasedProducts = b.ShoppingCartProducts
                        };

            return Json(new { data = query, success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetBill(int id)
        {
            var billDb = _context.Bills.Include(x => x.ShoppingCartProducts).SingleOrDefault(x => x.Id == id);

            var userBill = new UserBillViewModel
            {
                PurchasedProducts = billDb.ShoppingCartProducts,
                Tax = billDb.Tax,
                PurchaseDate = billDb.PurchaseDate,
                TotalPrice = billDb.TotalPrice
            };

            return View("_UserBill", userBill);
        }
    }
}