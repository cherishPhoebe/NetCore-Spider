using System;

namespace ZY.Application.HouseApp.Dtos
{
    public class PerSaleInfoDto
    {
        public Guid Id { get; set; }

        public Guid HouseId { get; set; }

        public string License { get; set; }

        public string IssueDate { get; set; }

        public string BindBuilding { get; set; }
    }
}