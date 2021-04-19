using FurnitureStore.CustomValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace FurnitureStore.Models
{
    /// <summary>
    /// Predstavlja proizvod koji salon poseduje
    /// </summary>
    public class Product
    {
        #region Properties
        /// <summary>
        /// Primarni kljuc u tabeli za proizvod
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Sifra proizvoda
        /// </summary>
        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(30, ErrorMessage = "Maksimalna dužina je 30 karaktera" )]
        [Display(Name = "Šifra proizvoda")]
        [Index(IsUnique = true)]
        public string ProductKey { get; set; }

        /// <summary>
        /// Naziv proizvoda
        /// </summary>
        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(50, ErrorMessage = "Maksimalna dužina je 50 karaktera")]
        [Display(Name = "Naziv")]
        public string ProductName { get; set; }

        /// <summary>
        /// Zemlja proizvodnje proizvoda
        /// </summary>
        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(100, ErrorMessage = "Maksimalna dužina je 100 karaktera")]
        [Display(Name = "Zemlja proizvodnje")]
        public string MadeIn { get; set; }

        /// <summary>
        /// Godina proizvodnje proizvoda
        /// </summary>
        [Required(ErrorMessage = "Polje je obavezno")]
        [Display(Name = "Godina proizvodnje")]
        public int ProductionYear { get; set; }

        /// <summary>
        /// Cena proizvoda
        /// </summary>
        [Required(ErrorMessage = "Polje je obavezno")]
        [Range(1, 1000000, ErrorMessage = "Minimalna cena je 1 maksimlana 1000000")]
        [Display(Name = "Cena")]
        public decimal Price { get; set; }

        /// <summary>
        /// Kolicina proizvoda u zalihama
        /// </summary>
        [Required(ErrorMessage = "Polje je obavezno")]
        [Range(0, 500, ErrorMessage = "Minimalna količina je 0 maksimlana 500")]
        [Display(Name = "Količina")]
        public int Quantity { get; set; }

        /// <summary>
        /// Strani kljuc ka tabeli za salon namestaja
        /// </summary>
        [Display(Name = "Prodajni salon")]
        public int ShopId { get; set; }

        /// <summary>
        /// Strani kljuc ka tabeli za tip proizvoda
        /// </summary>
        [Display(Name = "Tip nameštaja")]
        public int ProductTypeId { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Predstavlja salon namestaja koji poseduje dati proizvod
        /// </summary>
        public virtual Shop Shop { get; set; }

        /// <summary>
        /// Predstavlja tip proizvoda
        /// </summary>
        public virtual ProductType ProductType { get; set; }

        /// <summary>
        /// Predstavlja sliku proizvoda
        /// </summary>
        public virtual ICollection<UploadImage> UploadImages { get; set; }
        #endregion
    }
}