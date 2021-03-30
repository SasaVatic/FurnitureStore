using FurnitureStore.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using FurnitureStore.ViewModels;
using System.Web;
using System.Web.UI.WebControls;
using System;
using System.IO;

namespace FurnitureStore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly FurnitureStoreDbContext _context;
        public ProductsController()
        {
            _context = new FurnitureStoreDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetProducts()
        {
            var products = _context.Products.Include(x => x.Shop).Include(x => x.ProductType).ToList();
            var images = _context.UploadImages.ToList();

            var productsQuery = from p in products
                                from i in images
                                where p.Id == i.ProductId
                                select new
                                {
                                    Id = p.Id,
                                    ProductKey = p.ProductKey,
                                    ProductName = p.ProductName,
                                    MadeIn = p.MadeIn,
                                    ProductionYear = p.ProductionYear,
                                    Price = p.Price,
                                    Quantity = p.Quantity,
                                    ShopName = p.Shop.ShopName,
                                    ProductTypeName = p.ProductType.TypeName,
                                    Image = String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(i.ImageBytes))
                                };

            return Json(new { data = productsQuery }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                var product = new ProductFormViewModel
                {
                    ImageURL = "#",
                    Shops = _context.Shops.ToList(),
                    ProductTypes = _context.ProductTypes.ToList()
                };

                return View("ProductForm", product);
            }
            else
            {
                var productDb = _context.Products.SingleOrDefault(x => x.Id == id);
                var imageDb = _context.UploadImages.SingleOrDefault(x => x.ProductId == productDb.Id);

                var existingProduct = new ProductFormViewModel(productDb)
                {
                    ImageURL = String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(imageDb.ImageBytes)),
                    Shops = _context.Shops.ToList(),
                    ProductTypes = _context.ProductTypes.ToList()
                };

                return View("ProductForm", existingProduct);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEdit(ProductFormViewModel productFormVM)
        {
            if (productFormVM.Id == 0)
            {

                var newProduct = new Product
                {
                    Id = productFormVM.Id,
                    ProductKey = productFormVM.ProductKey,
                    ProductName = productFormVM.ProductName,
                    MadeIn = productFormVM.MadeIn,
                    ProductionYear = productFormVM.ProductionYear,
                    Price = productFormVM.Price,
                    Quantity = productFormVM.Quantity,
                    ShopId = productFormVM.ShopId,
                    ProductTypeId = productFormVM.ProductTypeId
                };

                _context.Products.Add(newProduct);
                
                var fileName = productFormVM.Image;

                // Da bi dodali sliku u Images folder. Zakomentarisano da bi samo u bazu sacuvao
                //fileName.SaveAs(Server.MapPath("~/Images/") + Guid.NewGuid() + Path.GetExtension(fileName.FileName));

                string newFileName = Guid.NewGuid() + Path.GetExtension(fileName.FileName);

                byte[] imageBytes = null;

                BinaryReader reader = new BinaryReader(fileName.InputStream);
                imageBytes = reader.ReadBytes(fileName.ContentLength);

                UploadImage uploadImage = new UploadImage
                {
                    ProductId = newProduct.Id,
                    ImageBytes = imageBytes,
                    NewName = newFileName,
                    OldName = fileName.FileName,
                    ImagePath = "~/Images/" + newFileName
                };

                _context.UploadImages.Add(uploadImage);

                _context.SaveChanges();

                return Json(new { success = true, message = "Uspešno sačuvano" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var productDb = _context.Products.SingleOrDefault(x => x.Id == productFormVM.Id);

                productDb.ProductKey = productFormVM.ProductKey;
                productDb.ProductName = productFormVM.ProductName;
                productDb.MadeIn = productFormVM.MadeIn;
                productDb.ProductionYear = productFormVM.ProductionYear;
                productDb.Price = productFormVM.Price;
                productDb.Quantity = productFormVM.Quantity;
                productDb.ShopId = productFormVM.ShopId;
                productDb.ProductTypeId = productFormVM.ProductTypeId;

                //Slika
                var imageDb = _context.UploadImages.SingleOrDefault(x => x.ProductId == productFormVM.Id);

                var fileName = productFormVM.Image;

                string newFileName = Guid.NewGuid() + Path.GetExtension(fileName.FileName);

                byte[] imageBytes = null;

                BinaryReader reader = new BinaryReader(fileName.InputStream);
                imageBytes = reader.ReadBytes(fileName.ContentLength);

                imageDb.ImageBytes = imageBytes;
                imageDb.NewName = newFileName;
                imageDb.OldName = fileName.FileName;
                imageDb.ImagePath = "~/Images/" + newFileName;

                _context.SaveChanges();

                return Json(new { success = true, message = "Uspešno izmenjeno" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Delete(int id)
        {
            var productDb = _context.Products.FirstOrDefault(x => x.Id == id);
            _context.Products.Remove(productDb);

            _context.SaveChanges();

            return Json(new { success = true, message = "Uspešno obrisano" }, JsonRequestBehavior.AllowGet);
        }
    }
}