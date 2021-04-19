using FurnitureStore.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using FurnitureStore.ViewModels;
using System.Web.UI.WebControls;
using System;
using System.IO;
using System.Collections.Generic;

namespace FurnitureStore.Controllers
{
    /// <summary>
    /// Kontroler za komade namestaja
    /// </summary>
    public class ProductsController : Controller
    {
        #region Konekcija ka bazi
        private readonly FurnitureStoreDbContext _context;
        public ProductsController()
        {
            _context = new FurnitureStoreDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion

        // GET: /Products
        #region Metoda za dobavljanje Index view-a komada namestaja
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.Admin))
            {
                return View();
            }
            return View("CustomerProductView");
        }
        #endregion

        // GET: /Products/GetProducts
        #region Metoda za dobavljanje podataka za tabelu komada namestaja
        [HttpGet]
        public ActionResult GetProducts()
        {

            var productsQuery = from u in _context.UploadImages.Include(x => x.Product).AsEnumerable()
                                select new
                                {
                                    Id = u.ProductId,
                                    ProductKey = u.Product.ProductKey,
                                    ProductName = u.Product.ProductName,
                                    MadeIn = u.Product.MadeIn,
                                    ProductionYear = u.Product.ProductionYear,
                                    Price = u.Product.Price,
                                    Quantity = u.Product.Quantity,
                                    ShopName = u.Product.Shop.ShopName,
                                    ProductTypeName = u.Product.ProductType.TypeName,
                                    Image = String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(u.ImageBytes))
                                };


            //var products = _context.Products.Include(x => x.Shop).Include(x => x.ProductType).ToList();

            //var images = _context.UploadImages.ToList();

            //var productsQuery = from p in products
            //                    from i in images
            //                    where p.Id == i.ProductId
            //                    select new
            //                    {
            //                        Id = p.Id,
            //                        ProductKey = p.ProductKey,
            //                        ProductName = p.ProductName,
            //                        MadeIn = p.MadeIn,
            //                        ProductionYear = p.ProductionYear,
            //                        Price = p.Price,
            //                        Quantity = p.Quantity,
            //                        ShopName = p.Shop.ShopName,
            //                        ProductTypeName = p.ProductType.TypeName,
            //                        Image = String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(i.ImageBytes))
            //                    };

            JsonResult result = Json(new { data = productsQuery }, JsonRequestBehavior.AllowGet);
            // Da bi mogao da vrati productQuery objekat koji ce biti veci od inicijalne MaxJsonLength koja je 2097152 karaktera ili 4MB
            // Namesteno na int max sto je 2147483647 da bi mogao sve podatke da vrati
            result.MaxJsonLength = int.MaxValue;

            return result;
        }
        #endregion

        // GET: /Products/AddOrEdit/1
        #region Metoda za dobavljanje ProductForm view-a za dodavanje ili izmenu komada namestaja 

        [HttpGet]
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult AddOrEdit(int id = 0)
        {
            #region popunjavanje liste za biranje godine
            var years = new List<string>();
            var date = DateTime.Now;
            int year = date.Year;

            for (int i = 0; i <= 10; i++)
            {
                years.Add(year.ToString());
                year--;
            }
            #endregion

            if (id == 0)
            {
                var product = new ProductFormViewModel
                {
                    ImageURL = "#",
                    Shops = _context.Shops.ToList(),
                    ProductTypes = _context.ProductTypes.ToList(),
                    Years = years
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
                    ProductTypes = _context.ProductTypes.ToList(),
                    Years = years
                };

                return View("ProductForm", existingProduct);
            }
        }
        #endregion

        // POST: /Products/AddOrEdit
        #region Metoda za dodavanje ili izmenu komada namestaja u bazi

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEdit(ProductFormViewModel productFormVM)
        {            
            if (productFormVM.Id == 0)
            {
                if (!ModelState.IsValid)
                {
                    if (!ModelState.IsValidField("ProductionYear") && !ModelState.IsValidField("ProductKey"))
                    {
                        return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    }
                    else if (!ModelState.IsValidField("ProductionYear"))
                    {
                        return Json(new { isYearNonValid = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { isKeyNonValid = true }, JsonRequestBehavior.AllowGet);
                    }
                }

                var newProduct = new Product
                {
                    Id = productFormVM.Id,
                    ProductKey = productFormVM.ProductKey,
                    ProductName = productFormVM.ProductName,
                    MadeIn = productFormVM.MadeIn,
                    ProductionYear = (int)productFormVM.ProductionYear,
                    Price = (int)productFormVM.Price,
                    Quantity = (int)productFormVM.Quantity,
                    ShopId = productFormVM.ShopId,
                    ProductTypeId = productFormVM.ProductTypeId
                };

                _context.Products.Add(newProduct);

                #region Dodavanje nove slike u bazu

                var newFile = productFormVM.Image;

                // Da bi dodali sliku u Images folder. Zakomentarisano da bi samo u bazu sacuvao
                //fileName.SaveAs(Server.MapPath("~/Images/") + Guid.NewGuid() + Path.GetExtension(fileName.FileName));

                string newFileName = Guid.NewGuid() + Path.GetExtension(newFile.FileName);

                byte[] imageBytes = null;

                BinaryReader reader = new BinaryReader(newFile.InputStream);
                imageBytes = reader.ReadBytes(newFile.ContentLength);

                UploadImage uploadImage = new UploadImage
                {
                    ProductId = newProduct.Id,
                    ImageBytes = imageBytes,
                    NewName = newFileName,
                    OldName = newFile.FileName,
                    ImagePath = "~/Images/" + newFileName
                };

                _context.UploadImages.Add(uploadImage);
                #endregion

                _context.SaveChanges();

                return Json(new { success = true, message = "Uspešno sačuvano" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var productDb = _context.Products.SingleOrDefault(x => x.Id == productFormVM.Id);

                productDb.ProductKey = productFormVM.ProductKey;
                productDb.ProductName = productFormVM.ProductName;
                productDb.MadeIn = productFormVM.MadeIn;
                productDb.ProductionYear = (int)productFormVM.ProductionYear;
                productDb.Price = (decimal)productFormVM.Price;
                productDb.Quantity = (int)productFormVM.Quantity;
                productDb.ShopId = productFormVM.ShopId;
                productDb.ProductTypeId = productFormVM.ProductTypeId;

                #region Izmena postojece slike u bazi

                var modifiedFile = productFormVM.Image;

                // ako je izmenjena od strane klijenta izvrsi upis u bazu
                if (modifiedFile != null)
                {
                    var imageDb = _context.UploadImages.SingleOrDefault(x => x.ProductId == productFormVM.Id);
                    string newFileName = Guid.NewGuid() + Path.GetExtension(modifiedFile.FileName);

                    byte[] imageBytes = null;

                    BinaryReader reader = new BinaryReader(modifiedFile.InputStream);
                    imageBytes = reader.ReadBytes(modifiedFile.ContentLength);

                    imageDb.ImageBytes = imageBytes;
                    imageDb.NewName = newFileName;
                    imageDb.OldName = modifiedFile.FileName;
                    imageDb.ImagePath = "~/Images/" + newFileName;
                }
                #endregion

                _context.SaveChanges();

                return Json(new { success = true, message = "Uspešno izmenjeno" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        // POST: /Products/Delete/1
        #region Metoda za brisanje komada namestaja iz baze

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var productDb = _context.Products.FirstOrDefault(x => x.Id == id);
            _context.Products.Remove(productDb);

            _context.SaveChanges();

            return Json(new { success = true, message = "Uspešno obrisano" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}