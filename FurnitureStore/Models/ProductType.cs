using FurnitureStore.CustomValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

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
        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(50, ErrorMessage = "Maksimalna dužina opisa je 500 karaktera")]
        [Display(Name = "Naziv")]
        [Index(IsUnique = true)]
        public string TypeName { get; set; }

        /// <summary>
        /// Opis tipa proizvoda
        /// </summary>
        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(500, ErrorMessage = "Maksimalna dužina opisa je 500 karaktera")]
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