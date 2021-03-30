using System;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    /// <summary>
    /// Predstavlja racun za kupljeni proizvod
    /// </summary>
    public class Bill
    {
        #region Properties
        /// <summary>
        /// Primarni kljuc u tabeli za racun
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Kupljena kolicina
        /// </summary>
        [Required(ErrorMessage = "Količina je obavezna")]
        [Range(1, 500)]
        [Display(Name = "Količina")]
        public int Quantity { get; set; }

        /// <summary>
        /// Cena kupljenog proizvoda
        /// </summary>        
        [Required]
        [Display(Name = "Cena")]
        public decimal Price { get; set; }

        /// <summary>
        /// Porez na kupljeni proizvod u procentima
        /// </summary>
        [Display(Name = "Porez")]
        [Range(0, 100)]
        public int Tax { get; set; }

        /// <summary>
        /// Ukupna cena sa porezom za dati proizvod
        /// </summary>
        [Display(Name = "Ukupna cena sa porezom")]
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Datum kupovine proizvoda
        /// </summary>
        [Display(Name = "Datum i vreme kupovine")]
        public DateTime PurchaseDate { get; set; }

        /// <summary>
        /// Naziv salona u kom je proizvod kupljen
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "Prodajni salon")]
        public string ShopName { get; set; }

        /// <summary>
        /// Strani kljuc ka drugoj tabeli za korisnika koji kupuje
        /// </summary>
        [Display(Name = "Ime kupca")]
        public int UserId { get; set; }

        /// <summary>
        /// Strani kljuc ka drugoj tabeli za proizvod koji se kupuje
        /// </summary>
        [Display(Name = "Naziv proizvoda")]
        public int ProductId { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Predstavlja proizvod koji je kupljen
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Predstavlja korisnika koji je kupio proizvod
        /// </summary>
        public virtual User User { get; set; }
        #endregion
    }
}