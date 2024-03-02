using GroupLucky.Domain.Common;

namespace GroupLucky.Domain
{
    public class Category : BaseDomainModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
