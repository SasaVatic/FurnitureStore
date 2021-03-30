using FurnitureStore.Models;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.ViewModels
{
    public class ShopFormViewModel
    {
        #region Properties
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        [Display(Name = "Naziv")]
        public string ShopName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Vlasnik")]
        public string OwnerName { get; set; }
       
        [Required]
        [StringLength(10)]
        [Display(Name = "Broj telefona")]
        [RegularExpression(@"^[0-9]{10}$")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(255)]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(253)]
        [Url]
        [Display(Name = "Web stranica")]
        public string WebPageURL { get; set; }

        [Required]
        [Display(Name = "Poreski identifikacioni broj")]
        [RegularExpression(@"^[0-9]{8}$")]
        public int PIB { get; set; }

        [Required]
        [StringLength(13)]
        [Display(Name = "Broj žiro računa")]
        [RegularExpression(@"^[0-9]{3}-[0-9]{6}-[0-9]{2}$")]
        public string BZR { get; set; }
        public AddressViewModel Address { get; set; }
        #endregion

        #region Constructors
        public ShopFormViewModel() { }
        public ShopFormViewModel(Shop shop)
        {
            Id = shop.Id;
            ShopName = shop.ShopName;
            OwnerName = shop.OwnerName;
            PhoneNumber = shop.PhoneNumber;
            Email = shop.Email;
            WebPageURL = shop.WebPageURL;
            PIB = shop.PIB;
            BZR = shop.BZR;
        }
        #endregion
    }
}