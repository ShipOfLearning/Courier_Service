using Courier_Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier_Service.Services.Offers
{
    public class OfferProvider : IOfferProvider
    {
        private readonly List<IOfferRule> _offerRules = new();

        public OfferProvider()
        {
            _offerRules.Add(new OfferOFR001());
            _offerRules.Add(new OfferOFR002());
            _offerRules.Add(new OfferOFR003());
        }
        public IOfferRule GetOffer(string code)
        {
            return _offerRules.FirstOrDefault(r => r.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
        }
        
        public IEnumerable<IOfferRule> GetAllOffers()
        {
            return _offerRules;
        }

    }
}
