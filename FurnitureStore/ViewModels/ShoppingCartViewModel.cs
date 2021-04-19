using FurnitureStore.Models;
using System.Collections.Generic;

namespace FurnitureStore.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<ShoppingCartProduct> CartProducts { get; set; }
        public decimal BillTotalPrice { get; set; }
        public ShoppingCartViewModel()
        {
            CartProducts = new List<ShoppingCartProduct>();
        }
    }
}