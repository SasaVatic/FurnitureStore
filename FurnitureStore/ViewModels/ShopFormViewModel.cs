using FurnitureStore.CustomValidation;
using FurnitureStore.Models;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.ViewModels
{
    public class ShopFormViewModel
    {
        #region Properties
        public int Id { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(50, ErrorMessage = "Maksimalna dužina naziva je 50 karaktera")]
        [Display(Name = "Naziv")]
        public string ShopName { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(50, ErrorMessage = "Maksimalna dužina imena je 50 karaktera")]
        [Display(Name = "Vlasnik")]
        public string OwnerName { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(10)]
        [Display(Name = "Broj telefona")]
        [RegularExpression(@"^[0-9]{9,10}$", ErrorMessage = "Broj mora imati 10 jednocifrenih brojeva")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(255, ErrorMessage = "Maksimalna dužina URL je 255 minimalna 3 karaktera", MinimumLength = 3)]
        [EmailAddress(ErrorMessage = "Nije validan email format")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(253, ErrorMessage = "Maksimalna dužina URL je 253 a minimalna 18 karaktera", MinimumLength = 18)]
        [Url(ErrorMessage = "Niste uneli validan format za URL, format mora biti https://www.nazivsajta.com")]
        [Display(Name = "Web stranica")]
        public string WebPageURL { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [Display(Name = "Poreski identifikacioni broj")]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "PIB mora imati 9 brojeva")]
        [UniquePIB(ErrorMessage = "Salon sa istim poreskim brojem već postoji")]
        public int? PIB { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(13)]
        [Display(Name = "Broj žiro računa")]
        [RegularExpression(@"^[0-9]{3}-[0-9]{6}-[0-9]{2}$", ErrorMessage = "Format mora biti xxx-xxxxxx-xx")]
        [UniqueBZR(ErrorMessage = "Salon sa istim brojem žiro računa već postoji")]
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