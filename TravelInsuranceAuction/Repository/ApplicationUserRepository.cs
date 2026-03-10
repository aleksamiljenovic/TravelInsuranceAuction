using TravelInsuranceAuction.Data;
using TravelInsuranceAuction.Models;
using TravelInsuranceAuction.Repository.IRepository;

namespace TravelInsuranceAuction.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Update(ApplicationUser obj)
        {
            _db.ApplicationUsers.Update(obj);
        }
    }
}
