using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    /// <summary>
    /// Predstavlja korisnika online prodavnice
    /// </summary>
    public class User
    {
        #region Properties
        /// <summary>
        /// Primarni kljuc u tabeli za korisnika
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Korisnicko ime za korisnika
        /// </summary>
        [Required]
        [StringLength(50)]
        //[Remote("UserExists", "Users", ErrorMessage = "User Name already in use")]
        [Display(Name = "Korisničko ime")]
        public string Username { get; set; }

        /// <summary>
        /// Lozinka korisnika
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "Lozinka")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,20}$")]
        public string Password { get; set; }

        /// <summary>
        /// Ime korisnika
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "Ime")]
        public string Name { get; set; }

        /// <summary>
        /// Prezime korisnika
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "Prezime")]
        public string Surname { get; set; }

        /// <summary>
        /// E-mail adresa korisnika
        /// </summary>
        [Required]
        [StringLength(255)]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        /// <summary>
        /// Strani kljuc ka drugoj tabeli za ulogu korisnika
        /// </summary>
        [Display(Name = "Uloga")]
        public byte UserRoleId { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Predstavlja ulogu koju korisnik ima na sajtu
        /// </summary>
        public virtual UserRole UserRole { get; set; }

        /// <summary>
        /// Predstavlja adresu na kojoj korisnik stanuje
        /// </summary>
        public virtual ICollection<Address> Addresses { get; set; }

        /// <summary>
        /// Predstavlja sve racune korisnika za kupljene proizvode
        /// </summary>
        public virtual ICollection<Bill> Bills { get; set; }
        #endregion
    }
}