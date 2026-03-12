using TravelInsuranceAuction.Models;

namespace TravelInsuranceAuction.Repository.IRepository
{
    public interface IOfferRepository:IRepository<Offer>
    {
        void Update(Offer obj);
    }
}
