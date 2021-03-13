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
        [Required]
        [Display(Name = "Cena")]
        public decimal Price { get; set; }
        /// <summary>
        /// Kupljena kolicina proizvoda
        /// </summary>
        [Required]
        [Display(Name = "Količina")]
        public int Quantity { get; set; }
        /// <summary>
        /// Naziv salona u kom je proizvod kupljen
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "Prodajni salon")]
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