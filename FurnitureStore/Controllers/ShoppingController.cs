using FurnitureStore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System;
using Microsoft.AspNet.Identity;
using FurnitureStore.ViewModels;

namespace FurnitureStore.Controllers
{
    public class ShoppingController : Controller
    {
        #region Konekcija ka bazi
        private readonly FurnitureStoreDbContext _context;
        private ShoppingCartViewModel cart;
        public ShoppingController()
        {
            _context = new FurnitureStoreDbContext();
            cart = new ShoppingCartViewModel();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion

        // GET: /Shopping/GetCartProducts
        [HttpGet]
        public ActionResult GetCartProducts()
        {
            if (Session["CartProducts"] != null)
            {
                var cartProducts = (Session["CartProducts"] as ShoppingCartViewModel).CartProducts;

                var query = from c in cartProducts
                            select new
                            {
                                ProductName = c.ProductName,
                                ShopName = c.ShopName,
                                UnitPrice = c.UnitPrice,
                                Add = c.ProductId,
                                Quantity = c.Quantity,
                                Remove = c.ProductId,
                                TotalPrice = c.TotalPrice,
                                ProductId = c.ProductId,
                                BillTotal = cartProducts.Sum(x => x.TotalPrice)
                            };


                return Json(new { data = query }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
        }

        // GET: /Shoping/ShoppingCart
        [HttpGet]
        public ActionResult ShoppingCart()
        {
            cart = Session["CartProducts"] as ShoppingCartViewModel;

            return View(cart);
        }

        // POST: /Shoping/AddToCart/1
        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            var productDb = _context.Products.Include(x => x.ProductType).Single(x => x.Id == id);
            ShoppingCartProduct cartProduct = new ShoppingCartProduct();

            // Ako korpa nije prazna dodeli cartProducts listi vrednost Session["CartProducts"]
            // koji u sebi ima sve do sada dodate proizvode
            if (Session["CartCounter"] != null)
            {
                cart = Session["CartProducts"] as ShoppingCartViewModel;
            }

            // Proveri da li proizvoda koji kupac hoce da kupi ima na stanju
            if (productDb.Quantity > 0)
            {
                // Ako u korpi vec ima odabranog proizvoda
                if (cart.CartProducts.Any(product => product.ProductId == id))
                {
                    // i kolicina koju kupac hoce da kupi je manja od one u zalihama
                    // samo dodaj na kolicinu jedan i izracunaj ukupnu cenu
                    if (cart.CartProducts.Single(x => x.ProductId == id).Quantity < productDb.Quantity)
                    {
                        cartProduct = cart.CartProducts.Single(x => x.ProductId == id);
                        cartProduct.Quantity += 1;
                        cartProduct.TotalPrice = cartProduct.Quantity * cartProduct.UnitPrice;

                        cart.BillTotalPrice += cartProduct.UnitPrice;
                    }
                    // u suprotnom vrati poruku da nema na stanju
                    else
                    {
                        return Json(new { success = false, message = "Nema na stanju", counter = cart.CartProducts.Count }, JsonRequestBehavior.AllowGet);
                    }
                }
                // Ako proizvod nije vec dodat u korpu napravi novi ShoppingCartProduct i dodaj ga u korpu
                else
                {
                    cartProduct.ProductId = productDb.Id;
                    cartProduct.ProductName = productDb.ProductName;
                    cartProduct.ProductTypeName = productDb.ProductType.TypeName;
                    cartProduct.UnitPrice = productDb.Price;
                    cartProduct.TotalPrice = productDb.Price;
                    cartProduct.Quantity = 1;
                    cartProduct.ShopName = productDb.Shop.ShopName;

                    cart.CartProducts.Add(cartProduct);

                    // Ako vec ima proizvoda u korpi dodaj na ukupnu cenu
                    if (cart.BillTotalPrice != 0)
                    {
                        cart.BillTotalPrice += productDb.Price;
                    }
                    // u suprotnom inicijalizuj
                    else
                    {
                        cart.BillTotalPrice = productDb.Price;
                    }
                }
            }
            // Ako proizvoda nema na stanju a korisnik hoce da ga dodao u korpu vrati mu poruku
            // da proizvoda nema na stanju
            else
            {
                return Json(new { success = false, message = "Nema na stanju", counter = cart.CartProducts.Count }, JsonRequestBehavior.AllowGet);
            }

            // Session CartCounter koristimo da bi kupac imao uvida koliko vrsta proizvoda ima u korpi
            Session["CartCounter"] = cart.CartProducts.Count;
            // Session CartProducts koristimo da bi sacuvali sve proizvode koje je kupac dodao u korpu
            Session["CartProducts"] = cart;

            var billTotalPrice = cart.BillTotalPrice;

            return Json(new { success = true, message = "Dodato u korpu", counter = cart.CartProducts.Count, total = billTotalPrice }, JsonRequestBehavior.AllowGet);
        }

        // POST: /Shopping/RemoveFromCart/1
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            cart = Session["CartProducts"] as ShoppingCartViewModel;

            // Ako korpa nije null
            if (cart.CartProducts != null)
            {
                // Ukupna cena za racun
                var billTotalPrice = cart.BillTotalPrice;

                // Uzmi korpu
                var cartProducts = cart.CartProducts;
                // Proveri da li ima nesto u nnjoj
                if (cartProducts.Any())
                {
                    // Pronadji proizvod sa prosledjenim id-jem
                    var product = cartProducts.FirstOrDefault(x => x.ProductId == id);
                    // Ako nisi nasao vrati odgovarajuci response
                    if (product == null)
                    {
                        return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    }

                    // Skini za jedan sa racuna i koriguj cene u odnosu na to
                    product.Quantity--;
                    product.TotalPrice -= product.UnitPrice;

                    // Oduzmi od ukupnog za racun
                    billTotalPrice -= product.UnitPrice;
                    cart.BillTotalPrice = billTotalPrice;

                    // Ako je kolicina posle oduzimanja jednaka nula
                    if (product.Quantity == 0)
                    {
                        // Ukloni proizvod iz korpe
                        cartProducts.Remove(product);

                        // Ako je posle uklanjanja proizvoda korpa prazna
                        // vrati success false na osnovu kog ce view da refreshuje stranicu 
                        if (cartProducts.Count() == 0)
                        {
                            // Osvezi Session
                            Session["CartCounter"] = 0;
                            Session["CartProducts"] = cart;

                            return Json(new { success = false, counter = 0, total = "" }, JsonRequestBehavior.AllowGet);
                        }

                        // Osvezi Session
                        Session["CartProducts"] = cart;
                        Session["CartCounter"] = cart.CartProducts.Count;

                        // u suprotnom osvezi tabelu i ukupnu cenu
                        return Json(new { success = true, counter = cart.CartProducts.Count, total = billTotalPrice }, JsonRequestBehavior.AllowGet);
                    }

                    // Osvezi ukupnu cenu za korpu tj racun
                    cart.BillTotalPrice = billTotalPrice;

                    // Osvezi Session
                    Session["CartProducts"] = cart;
                    Session["CartCounter"] = cart.CartProducts.Count;

                    // osvezi tabelu i ukupnu cenu
                    return Json(new { success = true, counter = cart.CartProducts.Count, total = billTotalPrice }, JsonRequestBehavior.AllowGet);
                    
                }
            }

            // Ako nisi nista uradio vrati false
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        // POST: /Shopping/RemoveProduct/1
        [HttpPost]
        public ActionResult RemoveProduct(int id)
        {
            bool isCartEmpty = false; // da li je korpa prazna

            cart = Session["CartProducts"] as ShoppingCartViewModel;

            var product = cart.CartProducts.Single(x => x.ProductId == id);

            cart.CartProducts.Remove(product);

            cart.BillTotalPrice -= product.TotalPrice;

            // Osvezi Session
            Session["CartProducts"] = cart;
            Session["CartCounter"] = cart.CartProducts.Count;

            // ako je korpa prazna podesi isCartEmpty na true i Session na 0
            if (!cart.CartProducts.Any())
            {
                isCartEmpty = true;
                Session["CartProducts"] = new ShoppingCartViewModel();
                Session["CartCounter"] = 0;
            }

            return Json(new { success = true, isEmpty = isCartEmpty, counter = cart.CartProducts.Count, total = cart.BillTotalPrice }, JsonRequestBehavior.AllowGet);
        }

        // POST: /Shopping/Buy
        [HttpPost]
        public ActionResult Buy()
        {
            // Proizvodi u korpi sa ukupnom cenom
            cart = Session["CartProducts"] as ShoppingCartViewModel;
            
            // Ako kupac klikne kupi a korpa nije prazna
            if (cart.CartProducts.Any())
            {
                decimal total = cart.BillTotalPrice;

                // Napravi racun
                var bill = new Bill
                {
                    Tax = 10,
                    TotalPrice = ((total / 100) * 10) + total,
                    PurchaseDate = DateTime.Now.Date,
                    UserId = User.Identity.GetUserId()
                };

                _context.Bills.Add(bill);

                // Svim proizvodima u korpi dodeli vrednost stranog kljuca za BillId
                // zatim dodaj kupljeni proizvod u bazu
                foreach (var product in cart.CartProducts)
                {
                    product.BillId = bill.Id;
                    _context.ShoppingCartProducts.Add(product);
                }

                // Svim proizvodima u bazi koji su kupljeni oduzmi od kolicine onoliko koliko je kupljeno
                foreach (var p in cart.CartProducts)
                {
                    _context.Products.Single(x => x.Id == p.ProductId).Quantity -= p.Quantity;
                }

                _context.SaveChanges();

                // Kupovina je obavljena resetuj Session
                Session["CartCounter"] = 0;
                Session["CartProducts"] = new ShoppingCartViewModel();                
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}