using FurnitureStore.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.ViewModels
{
    public class BillFormViewModel
    {
        #region Properties
        public int Id { get; set; }

        [Required]
        [Display(Name = "Količina")]
        public int Quantity { get; set; }
        
        [Required]
        [Display(Name = "Cena")]
        public decimal Price { get; set; }

        [Display(Name = "Porez")]
        [Range(0, 100)]
        public int Tax { get; set; }

        [Display(Name = "Ukupna cena sa porezom")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Datum i vreme kupovine")]
        public DateTime PurchaseDate { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Prodajni salon")]
        public string ShopName { get; set; }

        [Display(Name = "Ime kupca")]
        public int UserId { get; set; }

        [Display(Name = "Naziv proizvoda")]
        public int ProductId { get; set; }
        #endregion

        #region Constructors
        public BillFormViewModel() { }
        #endregion
    }
}