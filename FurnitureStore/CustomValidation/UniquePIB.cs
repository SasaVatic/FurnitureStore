using FurnitureStore.Models;
using FurnitureStore.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FurnitureStore.CustomValidation
{
    public class UniquePIB : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            FurnitureStoreDbContext _context;
            var shop = (ShopFormViewModel)validationContext.ObjectInstance;

            using (_context = new FurnitureStoreDbContext())
            {
                try
                {
                    var pibDb = _context.Shops.FirstOrDefault(x => x.PIB == (int)value);
                    if (pibDb != null)
                    {
                        return new ValidationResult("Salon sa istim poreskim brojem već postoji");
                    }
                }
                catch (System.Exception)
                {
                    return new ValidationResult("Salon sa istim poreskim brojem već postoji");
                }
            }
            return ValidationResult.Success;
        }
    }
}