
namespace FurnitureStore.Models
{
    public class ShoppingCartProduct
    {
        #region Properties
        public int Id { get; set; }
        public int BillId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductTypeName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
        public string ShopName { get; set; }
        #endregion

        #region Navigation Properties
        public Bill Bill { get; set; }
        #endregion
    }
}