using TravelInsuranceAuction.Models;

namespace TravelInsuranceAuction.Repository.IRepository
{
    public interface IAutoBiddingSettingRepository : IRepository<AutoBiddingSetting>
    {
        void Update(AutoBiddingSetting obj);
    }
}
