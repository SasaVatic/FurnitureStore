using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    /// <summary>
    /// Predstavlja salon namestaja
    /// </summary>
    public class Shop
    {
        #region Properties
        /// <summary>
        /// Primarni kljuc tabele za salon
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Naziv salona
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "Naziv")]
        public string ShopName { get; set; }

        /// <summary>
        /// Ime i prezime vlasnika
        /// </summary>
        [Required]
        [StringLength(100)]
        [Display(Name = "Vlasnik")]
        public string OwnerName { get; set; }

        /// <summary>
        /// Broj telefona salona
        /// </summary>        
        [Required]
        [StringLength(10)]
        [Display(Name = "Broj telefona")]
        [RegularExpression(@"^[0-9]{10}$")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Email adresa salona
        /// </summary>
        [Required]
        [StringLength(255)]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        /// <summary>
        /// Adresa web stranice salona
        /// </summary>
        [Required]
        [StringLength(253)]
        [Url]
        [Display(Name = "Web stranica")]
        public string WebPageURL { get; set; }

        /// <summary>
        /// Poreski identifikacioni broj salona
        /// </summary>
        [Required]
        [Display(Name = "Poreski identifikacioni broj")]
        [RegularExpression(@"^[0-9]{8}$")]
        public int PIB { get; set; }

        /// <summary>
        /// Broj ziro racuna salona
        /// </summary>
        [Required]
        [StringLength(13)]
        [Display(Name = "Broj žiro računa")]
        [RegularExpression(@"^[0-9]{3}-[0-9]{6}-[0-9]{2}$")]
        public string BZR { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Predstavlja adresu na kojoj se salon nalazi
        /// </summary>
        public virtual ICollection<Address> Addresses { get; set; }

        /// <summary>
        /// Predstavlja proizvode koje salon poseduje
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }
        #endregion
    }
}