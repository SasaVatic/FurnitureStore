using FurnitureStore.Models;
using System.Web.Mvc;
using System.Data.Entity;

namespace FurnitureStore.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        #region Konekcija ka bazi
        private readonly FurnitureStoreDbContext _context;
        public HomeController()
        {
            _context = new FurnitureStoreDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion
        public ActionResult Index()
        {
            // Ako je korisnik u ulozi admina redirektuj ga
            if (User.IsInRole(RoleName.Admin))
            {
                return RedirectToAction("Index", "Products");
            }
            // u suprotnom vrati home page
            return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}