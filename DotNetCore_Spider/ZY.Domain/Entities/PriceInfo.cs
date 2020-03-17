using System;

namespace ZY.Domain.Entities
{
    public class PriceInfo:Entity
    {
        public Guid HouseId { get; set; }
        public string RecordDate { get; set; }

        public string AvgPrice { get; set; }

        public string StartingPrice { get; set; }

        public string PriceDescription { get; set; }
    }
}