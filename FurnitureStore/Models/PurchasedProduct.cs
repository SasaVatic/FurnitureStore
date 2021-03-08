using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureStore.Models
{
    /// <summary>
    /// Predstavlja kupljeni proizvod
    /// </summary>
    public class PurchasedProduct
    {
        #region Properties
        /// <summary>
        /// Primarni kljuc u tabeli za kupljeni proizvod
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Cena kupljenog proizvoda
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
        /// <summary>
        /// Strani kljuc ka drugoj tabeli za kupljeni proizvod
        /// </summary>
        public int BillId { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Predstavlja racun za kupljeni proizvod
        /// </summary>
        public virtual Bill Bill { get; set; }
        #endregion
    }
}