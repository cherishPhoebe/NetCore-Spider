using System.Collections.Generic;

namespace CoreMVC_Spider.Models
{
    public class HouseViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Url { get; set; }

        public string HomeUrl { get; set; }

        public string Address { get; set; }

        public string Price { get; set; }

        public string Tel { get; set; }

        public string RoomType { get; set; }

        public string BuildingType { get; set; }
        public string Point { get; internal set; }

        public string BaseInfoJson { get; set; }

        public List<PerSaleInfo> PerSaleList { get; set; }

        public List<PriceInfo> PriceList { get; set; }
        public string SaseInfoJson { get; internal set; }
    }

    public class PerSaleInfo {
        public string License { get; set; }

        public string IssueDate { get; set; }

        public string BindBuilding { get; set; }
    }

    public class PriceInfo {
        public string RecordDate { get; set; }

        public string AvgPrice { get; set; }

        public string StartingPrice { get; set; }

        public string PriceDescription { get; set; }
    }

}
