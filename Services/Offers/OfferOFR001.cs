using Courier_Service.Interface;
using Courier_Service.Model;

namespace Courier_Service.Services.Offers
{
    internal class OfferOFR001 : IOfferRule
    {
        public string Code => "OFR001";

        public bool IsApplicable(Package pkg)
        {
            return pkg.Weight >= 70 && pkg.Weight <= 200 &&
                    pkg.Distance <= 200;
        }

        public decimal CalculateDiscount(decimal baseDeliveryCost, Package pkg)
        {
            if (!IsApplicable(pkg)) return 0m;

            var cost = baseDeliveryCost + (pkg.Weight * 10m) + (pkg.Distance * 5m);
            return Math.Round(cost * 0.10m, 2);
        }

    }
}
