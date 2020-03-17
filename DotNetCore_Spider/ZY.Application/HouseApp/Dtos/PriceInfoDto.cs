using System;

namespace ZY.Application.HouseApp.Dtos
{
    public class PriceInfoDto
    {
        public Guid Id { get; set; }

        public Guid HouseId { get; set; }

        public string RecordDate { get; set; }

        public string AvgPrice { get; set; }

        public string StartingPrice { get; set; }

        public string PriceDescription { get; set; }
    }
}