using System.Collections.Generic;

namespace ZY.Application.HouseApp.ViewModel
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
        public string Point { get;  set; }

        public string BaseInfoJson { get; set; }

        public List<PerSaleInfoViewModel> PerSaleList { get; set; }

        public List<PriceInfoViewModel> PriceList { get; set; }
        public string SaseInfoJson { get;  set; }
    }

    public class PerSaleInfoViewModel {
        public string License { get; set; }

        public string IssueDate { get; set; }

        public string BindBuilding { get; set; }
    }

    public class PriceInfoViewModel {
        public string RecordDate { get; set; }

        public string AvgPrice { get; set; }

        public string StartingPrice { get; set; }

        public string PriceDescription { get; set; }
    }

}
