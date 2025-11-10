using Courier_Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier_Service.Interface
{
    public interface IDeliveryCostCalculator
    {
        decimal CalculateDeliveryCost(decimal baseDeliveryCost, Package pkg);
        decimal CalculateDiscount(decimal baseDeliveryCost, Package pkg);
    }
}
