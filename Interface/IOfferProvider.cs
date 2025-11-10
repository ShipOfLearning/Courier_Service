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
        IEnumerable<IOfferRule> GetAllOffers();
    }
}
