using GroupLucky.Domain.Common;

namespace GroupLucky.Domain
{
    public class Provider : BaseDomainModel
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int DocumentTypeId { get; set; }
        public string DocumentNumber { get; set; } = null!;
        public string? Address { get; set; }
        public string Phone { get; set; } = null!;
        public DocumentType DocumentType { get; set; }
    }
}
