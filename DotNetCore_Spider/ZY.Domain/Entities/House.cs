using System.Collections.Generic;

namespace ZY.Domain.Entities
{
    public class House : Entity
    {
        public string HouseKey { get; set; }

        public string Name { get; set; }
        public string Url { get; set; }

        public string HomeUrl { get; set; }

        public string Address { get; set; }

        public string Price { get; set; }

        public string Tel { get; set; }

        public string RoomType { get; set; }

        public string BuildingType { get; set; }
        public string Point { get;  set; }

        public string BaseInfoJson { get; set; }

        public List<PerSaleInfo> PerSaleList { get; set; }

        public List<PriceInfo> PriceList { get; set; }

        public string SaseInfoJson { get;  set; }
    }
}
