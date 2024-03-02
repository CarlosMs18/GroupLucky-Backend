using GroupLucky.Domain.Common;

namespace GroupLucky.Domain
{
    public class ProductStock : BaseDomainModel
    {
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int CurrentStock { get; set; }
        public decimal PurchasePrice { get; set; }
        public Product Product { get; set; } = null!;
        public Store Store { get; set; } = null!;
    }
}
