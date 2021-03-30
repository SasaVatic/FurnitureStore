using FurnitureStore.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.ViewModels
{
    public class UserFormViewModel
    {
        #region Properties
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Korisničko ime")]
        public string Username { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Lozinka")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,20}$")]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Ime")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Prezime")]
        public string Surname { get; set; }

        [Required]
        [StringLength(255)]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Uloga")]
        public byte UserRoleId { get; set; }

        public IEnumerable<UserRoleViewModel> UserRoles { get; set; }

        public AddressViewModel Address { get; set; }
        #endregion

        #region Constructors
        public UserFormViewModel() { }

        public UserFormViewModel(User user)
        {
            Id = user.Id;
            Username = user.Username;
            Password = user.Password;
            Name = user.Name;
            Surname = user.Surname;
            Email = user.Email;
            UserRoleId = user.UserRoleId;
        }
        #endregion
    }
}