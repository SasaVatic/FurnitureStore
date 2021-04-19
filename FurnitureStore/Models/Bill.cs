using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    /// <summary>
    /// Predstavlja racun za kupljeni proizvod
    /// </summary>
    public class Bill
    {
        #region Properties
        public int Id { get; set; }
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
        /// Strani kljuc ka drugoj tabeli za korisnika koji kupuje
        /// </summary>
        [Display(Name = "Ime kupca")]
        public string UserId { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Predstavlja proizvode koji su kupljeni
        /// </summary>
        public virtual ICollection<ShoppingCartProduct> ShoppingCartProducts { get; set; }

        /// <summary>
        /// Predstavlja korisnika koji je kupio proizvod
        /// </summary>
        public virtual User User { get; set; }
        #endregion
    }
}