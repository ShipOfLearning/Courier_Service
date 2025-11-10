using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier_Service.Interface
{
    internal interface IOfferProvider
    {
        IOfferRule GetOffer(string code);
        void RegisterOffers(IOfferRule rule);
        IEnumerable<IOfferRule> GetAllOffers();

    }
}
