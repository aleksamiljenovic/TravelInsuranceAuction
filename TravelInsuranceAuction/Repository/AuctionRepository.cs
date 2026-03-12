using TravelInsuranceAuction.Data;
using TravelInsuranceAuction.Models;
using TravelInsuranceAuction.Repository.IRepository;

namespace TravelInsuranceAuction.Repository
{
    public class AuctionRepository : Repository<Auction>,IAuctionRepository
    {
        private ApplicationDbContext _db;

        public AuctionRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Update(Auction obj)
        {
            _db.Auctions.Update(obj);
        }
    }
}
