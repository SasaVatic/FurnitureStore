using FurnitureStore.Models;
using FurnitureStore.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace FurnitureStore.Controllers
{
    public class ProductTypesController : Controller
    {
        private readonly FurnitureStoreDbContext _context;
        public ProductTypesController()
        {
            _context = new FurnitureStoreDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: ProductTypes
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetProductTypes()
        {
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

        [HttpGet]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEdit(ProductTypeFormViewModel productTypeFormVM)
        {
            if (productTypeFormVM.Id == 0)
            {
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
        public ActionResult Delete(int id)
        {
            var productTypeDb = _context.ProductTypes.FirstOrDefault(x => x.Id == id);
            _context.ProductTypes.Remove(productTypeDb);

            _context.SaveChanges();

            return Json(new { success = true, message = "Uspešno obrisano" }, JsonRequestBehavior.AllowGet);
        }
    }
}