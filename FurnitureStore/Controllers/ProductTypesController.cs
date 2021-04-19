using FurnitureStore.Models;
using FurnitureStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FurnitureStore.Controllers
{
    /// <summary>
    /// Kontroler za kategoriju namestaja
    /// </summary>
    public class ProductTypesController : Controller
    {
        #region Konekcija ka bazi
        private readonly FurnitureStoreDbContext _context;
        public ProductTypesController()
        {
            _context = new FurnitureStoreDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion

        // GET: /ProductTypes
        #region Metoda za dobavljanje Index view-a kategorije namestaja
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.Admin))
            {
                return View();
            }
            return View("CustomerProductTypeView");
        }
        #endregion

        // GET: /ProductTypes/GetProductTypes
        #region Metoda za dobavljanje podataka za popunu tabele kategorije namestaja
        [HttpGet]
        public ActionResult GetProductTypes()
        {
            //List<ProductType> pt = new List<ProductType>();
            //for (int i = 0; i < 15000; i++)
            //{
            //    pt.Add(new ProductType
            //    {
            //        TypeName = "Name" + i,
            //        Description = "Description" + i
            //    });
            //}

            //_context.ProductTypes.AddRange(pt);
            //_context.SaveChanges();

            var productTypes = _context.ProductTypes.ToList();

            var productsQuery = from p in productTypes
                                select new
                                {
                                    Id = p.Id,
                                    TypeName = p.TypeName,
                                    Description = p.Description
                                };

            return Json(new { data = productsQuery }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        // GET: /ProductTypes/AddOrEdit/1
        #region Metoda za dobavljanje ProductTypeForm view-a za dodavanje ili izmenu kategorije namestaja

        [HttpGet]
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                var newProductType = new ProductTypeFormViewModel();

                return View("ProductTypeForm", newProductType);
            }
            else
            {
                var productTypeDb = _context.ProductTypes.SingleOrDefault(x => x.Id == id);

                var existingProductType = new ProductTypeFormViewModel(productTypeDb);

                return View("ProductTypeForm", existingProductType);
            }
        }
        #endregion

        // POST: /ProductTypes/AddOrEdit
        #region Metoda za dodavanje ili izmenu kategorije namestaja u bazi

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEdit(ProductTypeFormViewModel productTypeFormVM)
        {
            
            if (productTypeFormVM.Id == 0)
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }

                var newProductType = new ProductType
                {
                    Id = productTypeFormVM.Id,
                    TypeName = productTypeFormVM.TypeName,
                    Description = productTypeFormVM.Description
                };

                _context.ProductTypes.Add(newProductType);

                _context.SaveChanges();

                return Json(new { success = true, message = "Uspešno sačuvano" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var productTypeDb = _context.ProductTypes.SingleOrDefault(x => x.Id == productTypeFormVM.Id);

                productTypeDb.TypeName = productTypeFormVM.TypeName;
                productTypeDb.Description = productTypeFormVM.Description;

                _context.SaveChanges();

                return Json(new { success = true, message = "Uspešno izmenjeno" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        // POST: /ProductTypes/Delete/1
        #region Metoda za brisanje kategorije namestaja iz baze

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var productTypeDb = _context.ProductTypes.FirstOrDefault(x => x.Id == id);
            _context.ProductTypes.Remove(productTypeDb);

            _context.SaveChanges();

            return Json(new { success = true, message = "Uspešno obrisano" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Validacija
        [HttpPost]
        public ActionResult IsTypeNameAvailable(string TypeName)
        {
            try
            {
                var typeName = _context.ProductTypes.Single(x => x.TypeName == TypeName);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}