using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureStore.Models
{
    /// <summary>
    /// Predstavlja kupljeni proizvod
    /// </summary>
    public class PurchasedProduct
    {
        #region Properties
        /// <summary>
        /// Predstavlja primarni kljuc u tabeli baze podataka za kupljeni proizvod
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Cenu kupljenog proizvoda
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Kupljena kolicina proizvoda
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Naziv salona u kom je proizvod kupljen
        /// </summary>
        public string ShopName { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Predstavlja racun za kupljeni proizvod
        /// </summary>
        public virtual Bill Bill { get; set; }
        #endregion
    }
}