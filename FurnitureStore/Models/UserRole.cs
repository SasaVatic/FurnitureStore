using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureStore.Models
{
    /// <summary>
    /// Predstavlja ulogu koju korisnik ima u online prodavnici
    /// </summary>
    public class UserRole
    {
        #region Properties
        /// <summary>
        /// Predstavlja primarni kljuc u tabeli baze podataka za ulogu korisnika
        /// </summary>
        public byte Id { get; set; }
        /// <summary>
        /// Tip uloge koji odredjuje ovlascenja koje korisnik ima na sajtu
        /// </summary>
        public string RoleType { get; set; }
        #endregion
    }
}