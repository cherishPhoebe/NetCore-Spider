using System;

namespace ZY.Domain.Entities
{
    public class PerSaleInfo:Entity
    {
        public Guid HouseId { get; set; }

        public string License { get; set; }

        public string IssueDate { get; set; }

        public string BindBuilding { get; set; }
    }
}