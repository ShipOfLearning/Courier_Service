using Courier_Service.Interface;
using Courier_Service.Model;

namespace Courier_Service.Services.Offers
{
    public class OfferOFR002 : IOfferRule
    {
        public string Code => "OFR002";

        public bool IsApplicable(Package pkg)
        {
            return pkg.Distance >= 50 && pkg.Distance <= 150 
                    && pkg.Weight >= 100 && pkg.Weight <= 250;
        }

        public decimal CalculateDiscount(decimal baseDeliveryCost, Package pkg)
        {
            if (!IsApplicable(pkg)) return 0m;
            var cost = baseDeliveryCost + (pkg.Weight * 10m) + (pkg.Distance * 5m);
            return Math.Round(cost * 0.07m, 2);
        }
    }
}
