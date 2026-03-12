using TravelInsuranceAuction.Models;

namespace TravelInsuranceAuction.Repository.IRepository
{
    public interface IAuctionRepository : IRepository<Auction>
    {
        void Update(Auction obj);
    }
}
