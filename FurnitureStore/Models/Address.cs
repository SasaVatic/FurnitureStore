using System.Collections.Generic;
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
        [Required]
        [StringLength(100)]
        [Display(Name = "Ulica")]        
        public string StreetName { get; set; }
        /// <summary>
        /// Broj ulice u kojoj se objekat nalazi
        /// </summary>
        [Required]
        [StringLength(4)]
        [Display(Name = "Broj")]
        [RegularExpression(@"^[a-zA-Z0-9]{1,4}$")]
        public string StreetNumber { get; set; }
        /// <summary>
        /// Postanski kod mesta u kom se objekat nalazi
        /// </summary>
        [Display(Name = "Poštanski kod")]
        [RegularExpression(@"^\d{5}$")]
        public int ZipCode { get; set; }
        /// <summary>
        /// Naziv mesta u kom se objekat nalazi
        /// </summary>
        [Required]
        [StringLength(100)]
        [Display(Name = "Mesto")]
        public string City { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Predstavlja korisnika koji zivi na ovoj adresi
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
        /// <summary>
        /// Predstavlja salon koji se nalazi na ovoj adresi
        /// </summary>
        public virtual ICollection<Shop> Shops { get; set; }
        #endregion
    }
}