using TravelInsuranceAuction.Data;
using TravelInsuranceAuction.Models;
using TravelInsuranceAuction.Repository.IRepository;

namespace TravelInsuranceAuction.Repository
{
    public class AgencyRepository: Repository<Agency>,IAgencyRepository
    {
        private ApplicationDbContext _db;

        public AgencyRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Update(Agency obj)
        {
            _db.Agencies.Update(obj);
        }
    }
}
