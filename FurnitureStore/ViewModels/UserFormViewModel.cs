using FurnitureStore.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.ViewModels
{
    public class UserFormViewModel
    {
        #region Properties
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Korisničko ime")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezna")]
        [StringLength(100, ErrorMessage = "{0} mora biti dužine bar {2} karaktera.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi lozinku")]
        [Compare("Password", ErrorMessage = "Lozinka i potvrda lozinke se ne slažu.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Ime")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Prezime")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email je obavezan")]
        [StringLength(255)]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Uloga je obavezna")]
        [Display(Name = "Uloga")]
        public string UserRoleId { get; set; }

        public IEnumerable<UserRoleViewModel> UserRoles { get; set; }

        public AddressViewModel Address { get; set; }
        #endregion

        #region Constructors
        public UserFormViewModel() { }

        public UserFormViewModel(User user)
        {
            Id = user.Id;
            Username = user.UserName;
            Name = user.Name;
            Surname = user.Surname;
            Email = user.Email;
        }
        #endregion
    }
}