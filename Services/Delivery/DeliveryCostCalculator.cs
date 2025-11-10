using Courier_Service.Interface;
using Courier_Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier_Service.Services.Delivery
{
    internal class DeliveryCostCalculator : IDeliveryCostCalculator
    {
        private readonly IOfferProvider _offerProvider;

        public DeliveryCostCalculator(IOfferProvider offerProvider)
        {
            _offerProvider = offerProvider;
        }

        public decimal CalculateDeliveryCost(decimal baseDeliveryCost, Package pkg)
        {
            var cost = baseDeliveryCost + (pkg.Weight * 10m) + (pkg.Distance * 5m);
            return cost;
        }

        public decimal CalculateDiscount(decimal baseDeliveryCost, Package pkg)
        {
            var rule = _offerProvider.GetOffer(pkg.OfferCode ?? string.Empty);
            if (rule == null) return 0m;
            return rule.CalculateDiscount(baseDeliveryCost, pkg);
        }
    }
}
