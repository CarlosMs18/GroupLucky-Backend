using GroupLucky.Domain.Common;

namespace GroupLucky.Domain
{
    public class Store : BaseDomainModel
    {
        public string? Name { get; set; }
        public ICollection<ProductStock> ProductsStocks { get; set; } = null;
    }
}
