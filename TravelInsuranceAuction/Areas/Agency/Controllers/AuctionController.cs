using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TravelInsuranceAuction.Data;
using TravelInsuranceAuction.Models;
using TravelInsuranceAuction.Repository.IRepository;
using TravelInsuranceAuction.ViewModels;

namespace TravelInsuranceAuction.Areas.Agency.Controllers
{
    [Area("Agency")]
    public class AuctionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;

        public AuctionController(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public IActionResult Index()
        {

            var auctions = _context.Auctions
        .Where(a => a.IsActive)
        .OrderByDescending(a => a.StartTime)
        .Include(a => a.InsuranceRequest)
        .ToList();

            var model = auctions.Select(a => new AuctionVM
            {
                Destination = a.InsuranceRequest.Destination,
                StartTime = a.StartTime,
                EndTime = a.EndTime,
            }).ToList();

            return View(model);

        }

        public IActionResult ClosedAuctions()
        {

            var auctions = _context.Auctions
        .Where(a => a.IsActive == false)
        .OrderByDescending(a => a.StartTime)
        .Include(a => a.InsuranceRequest)
        .ToList();

            var model = auctions.Select(a => new AuctionVM
            {
                Destination = a.InsuranceRequest.Destination,
                StartTime = a.StartTime,
                EndTime = a.EndTime,
            }).ToList();

            return View(model);

        }
        public IActionResult Statistics()
        {
            
            
            //var agencyId = user.AgencyId;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
            var agency = _unitOfWork.Agency.Get(a => a.Id == user.AgencyId);
            if (user == null || user.AgencyId == null)
                return NotFound();

            var offers = _unitOfWork.Offer
                .GetAll().Where(o => o.AgencyId == user.AgencyId)
                .ToList();
            var wonOffers = offers.Where(o => o.isWinning == true).ToList();


            var model = new AgencyStatisticsVM
            {
                AgencyName = user.Agency?.Name ?? "N/A",
                Won = offers.Count(o => o.isWinning == true),
                Lost = offers.Count(o => o.isWinning == false),
                Pending = offers.Count(o => o.isWinning == null),
                GrossEarnings = wonOffers.Sum(o => o.CurrentPrice),
                PlatformFee = wonOffers.Sum(o => o.CurrentPrice) * 0.10,
                TotalEarnings = wonOffers.Sum(o => o.CurrentPrice) * 0.90

            };

            return View(model);


        }

    }
}
