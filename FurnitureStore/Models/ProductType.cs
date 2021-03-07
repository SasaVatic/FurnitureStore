using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureStore.Models
{
    /// <summary>
    /// Predstavlja tip proizvoda
    /// </summary>
    public class ProductType
    {
        #region Properties
        /// <summary>
        /// Predstavlja primarni kljuc tabele u bazi podataka za tip proizvoda
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