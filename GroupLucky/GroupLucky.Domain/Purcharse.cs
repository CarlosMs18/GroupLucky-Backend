using GroupLucky.Domain.Common;

namespace GroupLucky.Domain
{
    public class Purcharse : BaseDomainModel
    {
        public string? Observation { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Igv { get; set; }
        public decimal TotalAmount { get; set; }
        public int WarehouseId { get; set; }
        public int ProviderId { get; set; }
        public ICollection<PurcharseDetail> PurcharseDetails { get; set; } = null!;
        public Provider? Provider { get; set; } = null!;
        public Store Store { get; set; } = null!;
    }
}
