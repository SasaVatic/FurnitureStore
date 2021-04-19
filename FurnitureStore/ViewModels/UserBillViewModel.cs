

using FurnitureStore.Models;
using System;
using System.Collections.Generic;

namespace FurnitureStore.ViewModels
{
    public class UserBillViewModel
    {
        public IEnumerable<ShoppingCartProduct> PurchasedProducts { get; set; }
        public int Tax { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}