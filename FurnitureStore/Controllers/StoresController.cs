using FurnitureStore.Models;
using FurnitureStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace FurnitureStore.Controllers
{
    [AllowAnonymous]
    public class StoresController : Controller
    {
        #region Konekcija ka bazi
        private readonly FurnitureStoreDbContext _context;
        public StoresController()
        {
            _context = new FurnitureStoreDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion
        // GET: Stores
        public ActionResult Index(int id)
        {
            List<Product> products = null;
            List<ProductViewModel> productVM = null;
            try
            {
                products = _context.Products.Include(x => x.UploadImages).Where(x => x.ShopId == id).ToList();
            }
            catch (System.Exception)
            {
                return RedirectToAction("Index", "Home");
            }
            if (products != null)
            {
                if (products.Any())
                {
                    productVM = new List<ProductViewModel>();
                    foreach (var product in products)
                    {
                        productVM.Add(new ProductViewModel
                        {
                            ProductName = product.ProductName,
                            TypeName = _context.ProductTypes.SingleOrDefault(x => x.Id == product.ProductTypeId).TypeName,
                            Price = product.Price,
                            Quantity = product.Quantity,
                            Image = String.Format("data:image/gif;base64,{0}", 
                            Convert.ToBase64String(_context.UploadImages.SingleOrDefault(x => x.ProductId == product.Id).ImageBytes))
                        });
                    }
                    return View("ShowProducts", productVM);
                }
            }
            return View("Index", "Home");
        }
    }
}