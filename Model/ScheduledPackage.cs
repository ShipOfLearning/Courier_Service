using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier_Service.Model
{
    public class ScheduledPackage
    {
        public string Id { get; set; }
        public decimal Weight { get; set; }
        public decimal Distance { get; set; }
        public decimal Discout { get; set; }
        public decimal TotalCost { get; set; }
        public decimal DeliveryTimeInHours { get; set; }

        public ScheduledPackage(string id, decimal weight, decimal distance, decimal discount, decimal totalCost, decimal deliveryTimeInHours)
        {
            Id = id;
            Weight = weight;
            Distance = distance;
            Discout = discount;
            TotalCost = totalCost;
            DeliveryTimeInHours = deliveryTimeInHours;
        }
    }
}
