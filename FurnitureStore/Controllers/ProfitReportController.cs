using FurnitureStore.Models;
using FurnitureStore.ViewModels;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Globalization;
using System;

namespace FurnitureStore.Controllers
{
    [Authorize(Roles = RoleName.Admin)]
    public class ProfitReportController : Controller
    {
        #region Konekcija ka bazi
        private readonly FurnitureStoreDbContext _context;
        public ProfitReportController()
        {
            _context = new FurnitureStoreDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion

        // GET: ProfitReport
        public ActionResult Index()
        {
            return View();
        }

        // GET: /ProfitReport/GenerateReport
        [HttpGet]
        public ActionResult GenerateReport()
        {
            var report = new ProfitReportViewModel
            {
                ProductTypes = _context.ProductTypes.ToList()
            };
            return View("ReportForm", report);
        }

        // POST: /ProfitReport/GenerateReport
        [HttpGet]
        public ActionResult GetReport(ProfitReportViewModel profitReportVM)
        {
            var anyProducts = _context.ShoppingCartProducts
                        .Where(x => x.Bill.PurchaseDate >= profitReportVM.StartDate.Value
                        && x.Bill.PurchaseDate <= profitReportVM.EndDate.Value
                        && x.ProductTypeName == profitReportVM.ProductTypeName).Any(); // da li ima u bazi i jedan prodati proizvod koji zadovoljava uslov

            if (anyProducts)
            {
                decimal totalProfit = _context.ShoppingCartProducts
                        .Where(x => x.Bill.PurchaseDate >= profitReportVM.StartDate.Value
                        && x.Bill.PurchaseDate <= profitReportVM.EndDate.Value
                        && x.ProductTypeName == profitReportVM.ProductTypeName).Sum(x => x.TotalPrice); // Suma zarade za celu kategoriju

                var query = from s in _context.ShoppingCartProducts.Include(x => x.Bill) // selektuj sve prodate proizvode
                            where s.Bill.PurchaseDate >= profitReportVM.StartDate.Value // gde je racun izdat u prosledjenom periodu
                            && s.Bill.PurchaseDate <= profitReportVM.EndDate.Value
                            && s.ProductTypeName == profitReportVM.ProductTypeName // i gde je kategorija jednaka prosledjenoj                        
                            group s by s.ShopName into p // grupisi filtrirane proizvode po nazivu salona iz kog su kupljeni
                            select new
                            {
                                ShopName = p.Key, // selektuj kljuc(je naziv salona) pod kojim su grupisani proizvodi
                                ProductSum = p.Sum(x => x.Quantity), // saberi Quantity propertije svih proizvoda
                                ProfitSum = p.Sum(x => x.TotalPrice), // saberi TotalPrice propertije svih proizvoda
                                ProductName = profitReportVM.ProductTypeName,
                                StartDate = profitReportVM.StartDate.Value,
                                EndDate = profitReportVM.EndDate.Value,
                                TotalProfitCategory = totalProfit
                            };

                return Json(new { data = query, success = true }, JsonRequestBehavior.AllowGet);
            }
            
            return Json(new { success = false, message = "Nema računa za zadati period" }, JsonRequestBehavior.AllowGet);
        }
    }
}