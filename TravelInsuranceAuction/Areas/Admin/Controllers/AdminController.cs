using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelInsuranceAuction.Data;
using TravelInsuranceAuction.Models;
using TravelInsuranceAuction.Repository.IRepository;
using TravelInsuranceAuction.ViewModels;

namespace TravelInsuranceAuction.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;

        public AdminController(IUnitOfWork unitOfWork,ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Verification()
        {
            var users = _unitOfWork.ApplicationUser.GetAll(includeProperties: "Agency").Where(u=>u.IsVerified==false).ToList();
           
            var model = users.Select(u=>new AdminVM
            {
                UserID = u.Id,
                AgencyName = u.Agency?.Name ?? "N/A",
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
            }).ToList();


            return View(model);
        }

        public IActionResult FinancialOverview()
        {
            var wonOffers = _context.Offers
                .Where(o => o.isWinning == true)
                .Include(o => o.Agency)
                .Include(o => o.Auction)
                .ToList();

            var totalGross = wonOffers.Sum(o => o.CurrentPrice);
            var platformFee = totalGross * 0.10;

            var byAgency = wonOffers
                .GroupBy(o => o.Agency?.Name ?? "N/A")
                .Select(g => new AgencyFinanceVM
                {
                    AgencyName = g.Key,
                    AuctionsWon = g.Count(),
                    GrossEarnings = g.Sum(o => o.CurrentPrice),
                    PlatformFee = g.Sum(o => o.CurrentPrice) * 0.10,
                    NetEarnings = g.Sum(o => o.CurrentPrice) * 0.90
                })
                .OrderByDescending(a => a.NetEarnings)
                .ToList();

            var model = new AdminFinanceVM
            {
                TotalGross = totalGross,
                TotalPlatformFee = platformFee,
                TotalAuctions = wonOffers.Count,
                ByAgency = byAgency
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult VerifyUser(string id)
        {
            var user = _unitOfWork.ApplicationUser.Get(u => u.Id == id);
            if (user == null)
                return NotFound();

            user.IsVerified = true;
            _unitOfWork.ApplicationUser.Update(user);
            _unitOfWork.Save();

            return RedirectToAction("Verification");
        }
    }
}
