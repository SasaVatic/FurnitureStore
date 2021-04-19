using FurnitureStore.CustomValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

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
        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(50, ErrorMessage = "Maksimalna dužina naziva je 50 karaktera")]
        [Display(Name = "Naziv")]
        public string ShopName { get; set; }

        /// <summary>
        /// Ime i prezime vlasnika
        /// </summary>
        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(50, ErrorMessage = "Maksimalna dužina imena je 50 karaktera")]
        [Display(Name = "Vlasnik")]
        public string OwnerName { get; set; }

        /// <summary>
        /// Broj telefona salona
        /// </summary>        
        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(10)]
        [Display(Name = "Broj telefona")]
        [RegularExpression(@"^[0-9]{9,10}$", ErrorMessage = "Broj mora imati izmedju 9 ili 10 jednocifrenih brojeva")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Email adresa salona
        /// </summary>
        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(255, ErrorMessage = "Maksimalna dužina URL je 255 a minimalna 3 karaktera", MinimumLength = 3)]
        [EmailAddress(ErrorMessage = "Nije validan email format")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        /// <summary>
        /// Adresa web stranice salona
        /// </summary>
        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(253, ErrorMessage = "Maksimalna dužina URL je 253 a minimalna 18 karaktera", MinimumLength = 18)]
        [Url(ErrorMessage = "Niste uneli validan format za URL, format mora biti https://www.nazivsajta.com")]
        [Display(Name = "Web stranica")]
        public string WebPageURL { get; set; }

        /// <summary>
        /// Poreski identifikacioni broj salona
        /// </summary>
        [Required(ErrorMessage = "Polje je obavezno")]
        [Display(Name = "Poreski identifikacioni broj")]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "PIB mora imati 9 brojeva")]
        [Index(IsUnique = true)]
        public int PIB { get; set; }

        /// <summary>
        /// Broj ziro racuna salona
        /// </summary>
        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(13)]
        [Display(Name = "Broj žiro računa")]
        [RegularExpression(@"^[0-9]{3}-[0-9]{6}-[0-9]{2}$", ErrorMessage = "Format mora biti xxx-xxxxxx-xx")]
        [Index(IsUnique = true)]
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