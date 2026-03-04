using TravelInsuranceAuction.Models;

namespace TravelInsuranceAuction.Repository.IRepository
{
    public interface IAgencyRepository:IRepository<Agency>
    {
        void Update(Agency obj);
    }
}
