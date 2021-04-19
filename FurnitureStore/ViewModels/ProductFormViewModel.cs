using FurnitureStore.CustomValidation;
using FurnitureStore.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace FurnitureStore.ViewModels
{
    public class ProductFormViewModel
    {
        #region Properties
        public int Id { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(30, ErrorMessage = "Maksimalna dužina je 30 karaktera")]
        [UniqueProductKey(ErrorMessage = "Proizvod sa istom šifrom već postoji")]
        [Display(Name = "Šifra proizvoda")]
        public string ProductKey { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(50, ErrorMessage = "Maksimalna dužina je 50 karaktera")]
        [Display(Name = "Naziv")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(100, ErrorMessage = "Maksimalna dužina je 100 karaktera")]
        [Display(Name = "Zemlja proizvodnje")]
        public string MadeIn { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [Display(Name = "Godina proizvodnje")]
        public int? ProductionYear { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [Range(1, 1000000, ErrorMessage = "Minimalna cena je 1 maksimalna 1000000")]
        [Display(Name = "Cena")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [Range(0, 500, ErrorMessage = "Minimalna količina je 0 maksimlana 500")]
        [Display(Name = "Količina")]
        public int? Quantity { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [Display(Name = "Prodajni salon")]
        public int ShopId { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [Display(Name = "Tip nameštaja")]
        public int ProductTypeId { get; set; }
        public IEnumerable<Shop> Shops { get; set; }
        public IEnumerable<ProductType> ProductTypes { get; set; }
        public HttpPostedFileWrapper Image { get; set; }
        public string ImageURL { get; set; }
        public List<string> Years { get; set; }
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