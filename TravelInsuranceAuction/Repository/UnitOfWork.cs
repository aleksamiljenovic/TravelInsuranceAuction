using TravelInsuranceAuction.Data;
using TravelInsuranceAuction.Models;
using TravelInsuranceAuction.Repository.IRepository;

namespace TravelInsuranceAuction.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public IInsuranceRequestRepository InsuranceRequest {  get; private set; }
        public IAgencyRepository Agency {  get; private set; }
        public IAutoBiddingSettingRepository AutoBiddingSetting {  get; private set; }
        public IApplicationUserRepository ApplicationUser {  get; private set; }
        public IAuctionRepository Auction {  get; private set; }
        public IOfferRepository Offer {  get; private set; }

        

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            InsuranceRequest = new InsuranceRequestRepository(_db);
            Agency = new AgencyRepository(_db);
            AutoBiddingSetting = new AutoBiddingSettingRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            Auction = new AuctionRepository(_db);
            Offer = new OfferRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
