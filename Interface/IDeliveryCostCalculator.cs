using Courier_Service.Model;

namespace Courier_Service.Interface
{
    public interface IDeliveryCostCalculator
    {
        decimal CalculateDeliveryCost(decimal baseDeliveryCost, Package pkg);
        decimal CalculateDiscount(decimal baseDeliveryCost, Package pkg);
    }
}
