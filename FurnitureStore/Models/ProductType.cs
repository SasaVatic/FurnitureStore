using System.Collections.Generic;

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
        public string TypeName { get; set; }
        /// <summary>
        /// Opis tipa proizvoda
        /// </summary>
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