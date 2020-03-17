using System;
using System.Collections.Generic;
using System.Text;

namespace ZY.Application.HouseApp.Dtos
{
    public class HouseDto
    {
        public Guid Id { get; set; }
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

        public List<PerSaleInfoDto> PerSaleList { get; set; }

        public List<PriceInfoDto> PriceList { get; set; }

        public string SaseInfoJson { get; internal set; }

        public DateTime AddTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
