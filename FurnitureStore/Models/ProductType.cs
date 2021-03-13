using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    /// <summary>
    /// Predstavlja tip proizvoda
    /// </summary>
    public class ProductType
    {
        #region Properties
        /// <summary>
        /// Primarni kljuc tabele za tip proizvoda
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Naziv tipa proizvoda
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "Naziv")]
        public string TypeName { get; set; }
        /// <summary>
        /// Opis tipa proizvoda
        /// </summary>
        [Required]
        [StringLength(500)]
        [Display(Name = "Opis")]
        public string Description { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Predstavlja vise komada proizvoda jednog tipa
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }
        #endregion
    }
}