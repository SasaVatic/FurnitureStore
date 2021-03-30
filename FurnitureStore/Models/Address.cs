using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    /// <summary>
    /// Predstavlja adresu na kojoj se objekat nalazi
    /// </summary>
    public class Address
    {
        #region Properties
        /// <summary>
        /// Primarni kljuc u tabeli za adresu
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Naziv ulice u kojoj se objekat nalazi
        /// </summary>
        [Required(ErrorMessage = "Naziv ulice je obavezan")]
        [StringLength(100, ErrorMessage = "Naziv ulice ne sme da sadrži više od 100 karaktera")]
        [Display(Name = "Ulica")]
        public string StreetName { get; set; }

        /// <summary>
        /// Broj ulice u kojoj se objekat nalazi
        /// </summary>
        [Required(ErrorMessage = "Broj ulice je obavezan")]
        [StringLength(maximumLength: 4, ErrorMessage = "Minimalna dužina broja je 1, maksimalna 4 karaktera", MinimumLength = 1)]
        [Display(Name = "Broj")]
        [RegularExpression(@"^[a-zA-Z0-9]{1,4}$")]
        public string StreetNumber { get; set; }

        /// <summary>
        /// Postanski kod mesta u kom se objekat nalazi
        /// </summary>
        [Required(ErrorMessage = "Poštanski kod je obavezan")]
        [Display(Name = "Poštanski kod")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "ZIP kod mora biti petocifreni broj")]
        public int ZipCode { get; set; }

        /// <summary>
        /// Naziv mesta u kom se objekat nalazi
        /// </summary>
        [Required(ErrorMessage = "Mesto stanovanja je obavezno")]
        [StringLength(maximumLength: 100, ErrorMessage = "Mesto stanovanja mora biti između 1 i 100 slova", MinimumLength = 1)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Mesto može sadržati samo slova")]
        [Display(Name = "Mesto")]
        public string City { get; set; }

        /// <summary>
        /// Strani kljuc ka tabeli Users
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Strani kljuc ka tabeli Shops
        /// </summary>
        public int? ShopId { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Predstavlja korisnika koji zivi na ovoj adresi
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Predstavlja salon koji se nalazi na ovoj adresi
        /// </summary>
        public virtual Shop Shop { get; set; }
        #endregion
    }
}