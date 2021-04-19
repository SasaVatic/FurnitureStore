using FurnitureStore.Models;
using FurnitureStore.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FurnitureStore.CustomValidation
{
    public class UniqueProductKey : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            FurnitureStoreDbContext _context;
            var product = (ProductFormViewModel)validationContext.ObjectInstance;

            using (_context = new FurnitureStoreDbContext())
            {
                try
                {
                    var productDb = _context.Products.FirstOrDefault(x => x.ProductKey == (string)value);
                    if (productDb != null)
                    {
                        return new ValidationResult("Proizvod sa datom šifrom postoji");
                    }
                }
                catch (System.Exception)
                {
                    return new ValidationResult("Proizvod sa datom šifrom postoji");
                }
            }
            return ValidationResult.Success;
        }
    }
}