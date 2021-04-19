using FurnitureStore.Models;
using FurnitureStore.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FurnitureStore.CustomValidation
{
    public class UniqueBZR : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            FurnitureStoreDbContext _context;
            var shop = (ShopFormViewModel)validationContext.ObjectInstance;

            using (_context = new FurnitureStoreDbContext())
            {
                try
                {
                    var bzrDb = _context.Shops.FirstOrDefault(x => x.BZR == (string)value);
                    if (bzrDb != null)
                    {
                        return new ValidationResult("Salon sa istim poreskim brojem već postoji");
                    }
                }
                catch (System.Exception)
                {
                    return new ValidationResult("Salon sa istim poreskim brojem računa već postoji");
                }
            }
            return ValidationResult.Success;
        }
    }
}