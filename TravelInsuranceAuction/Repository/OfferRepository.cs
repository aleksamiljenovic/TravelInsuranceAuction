using TravelInsuranceAuction.Data;
using TravelInsuranceAuction.Models;
using TravelInsuranceAuction.Repository.IRepository;
namespace TravelInsuranceAuction.Repository
{
    public class OfferRepository : Repository<Offer>, IOfferRepository 
    {
        private ApplicationDbContext _db;

        public OfferRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Offer obj)
        {
            _db.Offers.Update(obj);
        }
    }
}
