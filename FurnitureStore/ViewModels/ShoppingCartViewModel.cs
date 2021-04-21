using FurnitureStore.Models;
using System.Collections.Generic;

namespace FurnitureStore.ViewModels
{
    public class ShoppingCartViewModel
    {
        // Proizvodi u korpi
        public List<ShoppingCartProduct> CartProducts { get; set; }

        // Ukupna cena za sve proizvode u korpi sa porezom
        public decimal BillTotalPriceWithTax { get; set; }

        // Ukupna cena za sve proizvode u korpi bez poreza
        public decimal BillTotalPriceWithOutTax { get; set; }
        public ShoppingCartViewModel()
        {
            CartProducts = new List<ShoppingCartProduct>();
        }
    }
}