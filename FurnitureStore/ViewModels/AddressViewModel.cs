using FurnitureStore.Models;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.ViewModels
{
    public class AddressViewModel
    {
        #region Properties
        public int Id { get; set; }

        [Required(ErrorMessage = "Naziv ulice je obavezan")]
        [StringLength(100, ErrorMessage = "Naziv ulice ne sme da sadrži više od 100 karaktera")]
        [Display(Name = "Ulica")]
        public string StreetName { get; set; }

        [Required(ErrorMessage = "Broj ulice je obavezan")]
        [StringLength(maximumLength: 4, ErrorMessage = "Minimalna dužina broja je 1, maksimalna 4 karaktera", MinimumLength = 1)]
        [Display(Name = "Broj")]
        [RegularExpression(@"^[a-zA-Z0-9]{1,4}$")]
        public string StreetNumber { get; set; }

        [Required(ErrorMessage = "Poštanski kod je obavezan")]
        [Display(Name = "Poštanski kod")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "ZIP kod mora biti petocifreni broj")]
        public int ZipCode { get; set; }

        [Required(ErrorMessage = "Mesto stanovanja je obavezno")]
        [StringLength(maximumLength: 100, ErrorMessage = "Mesto stanovanja mora biti između 1 i 100 slova", MinimumLength = 1)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Mesto može sadržati samo slova")]
        [Display(Name = "Mesto")]
        public string City { get; set; }
        #endregion

        #region Constructors
        public AddressViewModel() { }
        public AddressViewModel(Address address)
        {
            Id = address.Id;
            StreetName = address.StreetName;
            StreetNumber = address.StreetNumber;
            ZipCode = address.ZipCode;
            City = address.City;
        }
        #endregion
    }
}