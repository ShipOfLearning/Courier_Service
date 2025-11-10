using Courier_Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier_Service.Interface
{
    public interface IOfferRule
    {
        string Code { get; }
        bool IsApplicable(Package pck);

        decimal CalculateDiscount(decimal baseDeliveryCost, Package pck);
    }
}
