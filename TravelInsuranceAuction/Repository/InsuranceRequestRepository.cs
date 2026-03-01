using TravelInsuranceAuction.Data;
using TravelInsuranceAuction.Models;
using TravelInsuranceAuction.Repository.IRepository;

namespace TravelInsuranceAuction.Repository
{
    public class InsuranceRequestRepository : Repository<InsuranceRequest>, IInsuranceRequestRepository
    {
        private ApplicationDbContext _db;

        public InsuranceRequestRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
