using FurnitureStore.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "Polje je obavezno")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        // Izmenjeno sa Email na UserName posto je Identity Framework po default-u
        // namesten da prilikom registracije za UserName dodeli vrednost Email kojim
        // se korisnik registrovao, a kod nas u aplikaciji UserName i Email imaju
        // razlicite vrednosti
        [Required(ErrorMessage = "Polje je obavezno")]
        [Display(Name = "Korisničko ime")]
        [StringLength(256, ErrorMessage = "Minimalna dužina korisničkog imena je 1, maksimalna 256", MinimumLength = 1)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [DataType(DataType.Password, ErrorMessage = "Niste uneli validnu lozinku")]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [Display(Name = "Zapamti me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        // Uz originalan kod dodat UserName radi editovanja
        [Required(ErrorMessage = "Polje je obavezno")]
        [Display(Name = "Korisničko ime")]
        [StringLength(30,ErrorMessage = "Minimalna dužina korisničkog imena je 1, maksimalna 30 karaktera", MinimumLength = 1)]
        public string UserName { get; set; }

        // Uz originalan kod dodat Name
        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(50, ErrorMessage = "Minimalna dužina imena je 1, maksimalna 50 karaktera", MinimumLength = 1)]
        [Display(Name = "Ime")]
        public string Name { get; set; }

        // Uz originalan kod dodat Surname
        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(50, ErrorMessage = "Minimalna dužina prezimena je 1, maksimalna 50 karaktera", MinimumLength = 1)]
        [Display(Name = "Prezime")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [EmailAddress(ErrorMessage = "Niste uneli email u validnom formatu")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(100, ErrorMessage = "{0} mora biti dužine bar {2} karaktera", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "Lozinka mora biti dužine izmedju 6 i 100 karaktera sa bar jednim malim i jednim velikim slovom")]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [DataType(DataType.Password, ErrorMessage = "Lozinka i potvrda lozinke se ne slažu.")]
        [Display(Name = "Potvrdi lozinku")]
        [Compare("Password", ErrorMessage = "Lozinka i potvrda lozinke se ne slažu.")]
        public string ConfirmPassword { get; set; }

        // Uz originalan kod dodat AddressViewModel
        // jer se adresa unosi prilikom registracije
        public AddressViewModel Address { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} mora biti dužine bar {2} karaktera.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi lozinku")]
        [Compare("Password", ErrorMessage = "Lozinka i potvrda lozinke se ne slažu.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
