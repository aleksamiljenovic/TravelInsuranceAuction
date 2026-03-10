using TravelInsuranceAuction.Data;
using TravelInsuranceAuction.Models;
using TravelInsuranceAuction.Repository.IRepository;

namespace TravelInsuranceAuction.Repository
{
    public class AutoBiddingSettingRepository : Repository<AutoBiddingSetting>, IAutoBiddingSettingRepository
    {
        private ApplicationDbContext _db;

        public AutoBiddingSettingRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Update(AutoBiddingSetting obj)
        {
            _db.AutoBiddingSettings.Update(obj);
        }
    }
}
