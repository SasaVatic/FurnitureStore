using FurnitureStore.Models;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.ViewModels
{
    public class AddressViewModel
    {
        #region Properties
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Ulica")]
        public string StreetName { get; set; }

        [Required]
        [StringLength(4)]
        [Display(Name = "Broj")]
        [RegularExpression(@"^[a-zA-Z0-9]{1,4}$")]
        public string StreetNumber { get; set; }

        [Display(Name = "Poštanski kod")]
        [RegularExpression(@"^\d{5}$")]
        public int ZipCode { get; set; }

        [Required]
        [StringLength(100)]
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