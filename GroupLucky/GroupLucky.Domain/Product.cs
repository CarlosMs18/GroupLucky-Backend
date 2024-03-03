using GroupLucky.Domain.Common;

namespace GroupLucky.Domain
{
    public class Product : BaseDomainModel
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int StockMin { get; set; }
        public int StockMax { get; set; }
        public string? Image { get; set; }
        public decimal UnitSalePrice { get; set; }
        public int CategoryId { get; set; }
        public  Category Category { get; set; } 
        public virtual ICollection<ProductStock> ProductStocks { get; set; } = null!;
    }
}
