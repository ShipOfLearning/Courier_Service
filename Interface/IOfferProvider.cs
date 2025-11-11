namespace Courier_Service.Interface
{
    internal interface IOfferProvider
    {
        IOfferRule GetOffer(string code);
        IEnumerable<IOfferRule> GetAllOffers();
    }
}
