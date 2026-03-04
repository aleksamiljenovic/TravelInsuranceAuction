using TravelInsuranceAuction.Data;
using TravelInsuranceAuction.Repository.IRepository;

namespace TravelInsuranceAuction.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public IInsuranceRequestRepository InsuranceRequest {  get; private set; }
        public IAgencyRepository Agency {  get; private set; }

        

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            InsuranceRequest = new InsuranceRequestRepository(db);
            Agency = new AgencyRepository(db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
