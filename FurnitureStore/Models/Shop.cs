using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string ShopName { get; set; }
        /// <summary>
        /// Ime i prezime vlasnika
        /// </summary>
        public string OwnerName { get; set; }
        /// <summary>
        /// Broj telefona salona
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Email adresa salona
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Adresa web stranice salona
        /// </summary>
        public string WebPageURL { get; set; }
        /// <summary>
        /// Poreski identifikacioni broj salona
        /// </summary>
        public int PIB { get; set; }
        /// <summary>
        /// Broj ziro racuna salona
        /// </summary>
        public string BZR { get; set; }
        /// <summary>
        /// Strani kljuc ka drugoj tabeli za adresu salona
        /// </summary>
        public int AddressId { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Predstavlja adresu na kojoj se salon nalazi
        /// </summary>
        public virtual Address Address { get; set; }
        /// <summary>
        /// Predstavlja proizvode koje salon poseduje
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }
        #endregion
    }
}