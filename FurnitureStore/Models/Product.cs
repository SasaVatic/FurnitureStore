using System;

namespace FurnitureStore.Models
{
    /// <summary>
    /// Predstavlja proizvod koji salon poseduje
    /// </summary>
    public class Product
    {
        #region Properties
        /// <summary>
        /// Primarni kljuc u tabeli za proizvod
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Sifra proizvoda
        /// </summary>
        public int ProductKey { get; set; }
        /// <summary>
        /// Naziv proizvoda
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// Zemlja proizvodnje proizvoda
        /// </summary>
        public string MadeIn { get; set; }
        /// <summary>
        /// Godina proizvodnje proizvoda
        /// </summary>
        public DateTime ProductionDate { get; set; }
        /// <summary>
        /// Cena proizvoda
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Kolicina proizvoda u zalihama
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Slika proizvoda
        /// </summary>
        public byte[] Picture { get; set; }
        /// <summary>
        /// Strani kljuc ka tabeli za salon namestaja
        /// </summary>
        public int ShopId { get; set; }
        /// <summary>
        /// Strani kljuc ka tabeli za tip proizvoda
        /// </summary>
        public int ProductTypeId { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Predstavlja salon namestaja koji poseduje dati proizvod
        /// </summary>
        public virtual Shop Shop { get; set; }
        /// <summary>
        /// Predstavlja tip proizvoda
        /// </summary>
        public virtual ProductType ProductType { get; set; }
        #endregion
    }
}