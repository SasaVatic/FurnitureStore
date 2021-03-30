using FurnitureStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace FurnitureStore.ViewModels
{
    public class ProductFormViewModel
    {
        #region Properties
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Šifra proizvoda")]
        public string ProductKey { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Naziv")]
        public string ProductName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Zemlja proizvodnje")]
        public string MadeIn { get; set; }

        [Required]
        [Display(Name = "Godina proizvodnje")]
        public int ProductionYear { get; set; }

        [Required]
        [Display(Name = "Cena")]
        public decimal Price { get; set; }

        [Range(0, 500)]
        [Display(Name = "Količina")]
        public int Quantity { get; set; }
                
        [Display(Name = "Prodajni salon")]
        public int ShopId { get; set; }

        [Display(Name = "Tip nameštaja")]
        public int ProductTypeId { get; set; }
        public IEnumerable<Shop> Shops { get; set; }
        public IEnumerable<ProductType> ProductTypes { get; set; }
        public HttpPostedFileWrapper Image { get; set; }
        public string ImageURL { get; set; }
        #endregion

        #region Constructors
        public ProductFormViewModel() { }
        public ProductFormViewModel(Product product)
        {
            Id = product.Id;
            ProductKey = product.ProductKey;
            ProductName = product.ProductName;
            MadeIn = product.MadeIn;
            ProductionYear = product.ProductionYear;
            Price = product.Price;
            Quantity = product.Quantity;
            ShopId = product.ShopId;
            ProductTypeId = product.ProductTypeId;
        }
        #endregion
    }
}