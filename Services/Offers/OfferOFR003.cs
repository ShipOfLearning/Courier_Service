using Courier_Service.Interface;
using Courier_Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier_Service.Services.Offers
{
    public class OfferOFR003 : IOfferRule
    {
        public string Code => "OFR003";

        public bool IsApplicable(Package pkg)
        {
            return pkg.Distance >= 50 && pkg.Distance <= 250 &
                  pkg.Weight >= 10 && pkg.Weight <= 150;
        }

        public decimal CalculateDiscount(decimal baseDeliveryCost, Package pkg)
        {
            if (!IsApplicable(pkg)) return 0m;

            var cost = baseDeliveryCost + (pkg.Weight * 10m) + (pkg.Distance * 5m);
            return Math.Round(cost * 0.05m, 2);
        }
    }
}
