using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Username { get; set; }
        /// <summary>
        /// Lozinka korisnika
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Ime korisnika
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Prezime korisnika
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// E-mail adresa korisnika
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Strani kljuc ka drugoj tabeli za ulogu korisnika
        /// </summary>
        public byte UserRoleId { get; set; }
        /// <summary>
        /// Strani kljuc ka drugoj tabeli za adresu korisnika
        /// </summary>
        public int AddressId { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Predstavlja ulogu koju korisnik ima na sajtu
        /// </summary>
        public virtual UserRole UserRole { get; set; }
        /// <summary>
        /// Predstavlja adresu na kojoj korisnik stanuje
        /// </summary>
        public virtual Address Address { get; set; }
        /// <summary>
        /// Predstavlja sve racune korisnika za kupljene proizvode
        /// </summary>
        public virtual ICollection<Bill> Bills { get; set; }
        #endregion
    }
}