using GroupLucky.Domain.Common;

namespace GroupLucky.Domain
{
    public class DocumentType : BaseDomainModel
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Abbreviation { get; set; }
        public ICollection<Provider> Providers { get; set; }
    }
}
