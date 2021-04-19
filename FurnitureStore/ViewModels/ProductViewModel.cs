﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureStore.ViewModels
{
    public class ProductViewModel
    {
        public string ProductName { get; set; }
        public string TypeName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}