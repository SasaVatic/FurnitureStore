using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureStore.Models
{
    /// <summary>
    /// Predstavlja racun za kupljeni proizvod
    /// </summary>
    public class Bill
    {
        #region Properties
        /// <summary>
        /// Primarni kljuc u tabeli baze podataka za racun
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
        /// Datum kupovine
        /// </summary>
        public DateTime PurchaseDate { get; set; }
        /// <summary>
        /// Strani kljuc ka drugoj tabeli u bazi podataka za kupljeni proizvod
        /// </summary>
        public int PurchasedProductId { get; set; }
        /// <summary>
        /// Strani kljuc ka drugoj tabeli u bazi podataka za korisnika koji kupuje
        /// </summary>
        public int UserId { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Predstavlja proizvod koji je kupljen
        /// </summary>
        public virtual PurchasedProduct PurchasedProduct { get; set; }
        /// <summary>
        /// Predstavlja korisnika koji je kupio proizvod
        /// </summary>
        public virtual User User { get; set; }
        #endregion
    }
}