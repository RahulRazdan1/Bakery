namespace Bakery.Models
{
    public class PurchaseProduct
    {
        public PurchaseProduct()
        {
            IsActive = true;
        }

        #region properties, fields, enumerations
        public long ProductId { get; set; }
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public bool IsGlutenFree { get; set; }
        public bool IsActive { get; set; }
        public int Quantity { get; set; }
        #endregion
    }
}
