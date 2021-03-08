using System;
using System.Collections.Generic;

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
        /// Porez na kupljeni proizvod u procentima
        /// </summary>
        public int Tax { get; set; }
        /// <summary>
        /// Ukupna cena sa porezom za dati proizvod
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// Datum kupovine proizvoda
        /// </summary>
        public DateTime PurchaseDate { get; set; }
        /// <summary>
        /// Strani kljuc ka drugoj tabeli za korisnika koji kupuje
        /// </summary>
        public int UserId { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Predstavlja proizvode koji su kupljeni
        /// </summary>
        public virtual ICollection<PurchasedProduct> PurchasedProducts { get; set; }
        /// <summary>
        /// Predstavlja korisnika koji je kupio proizvod
        /// </summary>
        public virtual User User { get; set; }
        #endregion
    }
}