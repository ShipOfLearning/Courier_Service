using Courier_Service.Model;

namespace Courier_Service.Interface
{
    public interface IOfferRule
    {
        string Code { get; }
        bool IsApplicable(Package pck);

        decimal CalculateDiscount(decimal baseDeliveryCost, Package pck);
    }
}
