using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureStore.Models
{
    /// <summary>
    /// Predstavlja adresu na kojoj se objekat nalazi
    /// </summary>
    public class StreetAddress
    {
        #region Properties
        /// <summary>
        /// Predstavlja primarni kljuc u tabeli baze podataka za adresu
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Naziv ulice u kojoj se objekat nalazi
        /// </summary>
        public string StreetName { get; set; }
        /// <summary>
        /// Broj ulice u kojoj se objekat nalazi
        /// </summary>
        public string StreetNumber { get; set; }
        /// <summary>
        /// Postanski kod mesta u kom se objekat nalazi
        /// </summary>
        public int ZipCode { get; set; }
        /// <summary>
        /// Naziv mesta u kom se objekat nalazi
        /// </summary>
        public string City { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Predstavlja korisnika koji zivi na ovoj adresi
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// Predstavlja salon koji se nalazi na ovoj adresi
        /// </summary>
        public virtual Shop Shop { get; set; }
        #endregion
    }
}