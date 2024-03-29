﻿using GroupLucky.Domain.Common;

namespace GroupLucky.Domain
{
    public class PurcharseDetail : BaseDomainModel
    {
        public int PurcharseId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPurcharPrice { get; set; }
        public decimal Total { get; set; }
        public  Product? Product { get; set; } = null!;
        public  Purcharse? Purcharse { get; set; } = null!;
    }
}
