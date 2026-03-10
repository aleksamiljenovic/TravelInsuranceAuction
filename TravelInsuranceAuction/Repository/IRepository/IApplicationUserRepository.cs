using TravelInsuranceAuction.Models;

namespace TravelInsuranceAuction.Repository.IRepository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        void Update(ApplicationUser obj);
    }
}
