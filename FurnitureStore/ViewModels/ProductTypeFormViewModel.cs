using FurnitureStore.CustomValidation;
using FurnitureStore.Models;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.ViewModels
{
    public class ProductTypeFormViewModel
    {
        #region Properties
        public int Id { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(50, ErrorMessage = "Maksimalna dužina opisa je 500 karaktera")]
        [Display(Name = "Naziv")]
        [UniqueTypeName(ErrorMessage = "Kategorija sa istim nazivom već postoji")]
        public string TypeName { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(500, ErrorMessage = "Maksimalna dužina opisa je 500 karaktera")]
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