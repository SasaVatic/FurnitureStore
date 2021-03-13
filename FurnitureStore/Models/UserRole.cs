﻿
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    /// <summary>
    /// Predstavlja ulogu koju korisnik ima u prodavnici
    /// </summary>
    public class UserRole
    {
        #region Properties
        /// <summary>
        /// Primarni kljuc u tabeli za ulogu korisnika
        /// </summary>
        public byte Id { get; set; }
        /// <summary>
        /// Tip uloge koji odredjuje ovlascenja koje korisnik ima na sajtu
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "Uloga")]
        public string RoleType { get; set; }
        #endregion
    }
}