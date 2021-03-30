using FurnitureStore.Models;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.ViewModels
{
    public class ProductTypeFormViewModel
    {
        #region Properties
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Naziv")]
        public string TypeName { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Opis")]
        public string Description { get; set; }
        #endregion

        #region Constructors
        public ProductTypeFormViewModel() { }
        public ProductTypeFormViewModel(ProductType productType)
        {
            Id = productType.Id;
            TypeName = productType.TypeName;
            Description = productType.Description;
        }
        #endregion
    }
}