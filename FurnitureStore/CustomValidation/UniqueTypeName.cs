using FurnitureStore.Models;
using FurnitureStore.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FurnitureStore.CustomValidation
{
    public class UniqueTypeName : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            FurnitureStoreDbContext _context;
            var productType = (ProductTypeFormViewModel)validationContext.ObjectInstance;

            using (_context = new FurnitureStoreDbContext())
            {
                try
                {
                    var productTypeDb = _context.ProductTypes.FirstOrDefault(x => x.TypeName == (string)value);
                    if (productTypeDb != null)
                    {
                        return new ValidationResult("Kategorija sa istim nazivom već postoji");
                    }
                }
                catch (System.Exception)
                {
                    return new ValidationResult("Desila se greška prilikom pokušaja upisa");
                }
            }
            return ValidationResult.Success;
        }
    }
}