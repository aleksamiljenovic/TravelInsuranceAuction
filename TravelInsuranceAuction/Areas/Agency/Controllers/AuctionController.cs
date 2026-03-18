using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TravelInsuranceAuction.Data;
using TravelInsuranceAuction.Filters;
using TravelInsuranceAuction.Models;
using TravelInsuranceAuction.Repository.IRepository;
using TravelInsuranceAuction.Utility;
using TravelInsuranceAuction.ViewModels;

namespace TravelInsuranceAuction.Areas.Agency.Controllers
{
    [Area("Agency")]
    [Authorize(Roles = SD.Role_Agency)]
    [ServiceFilter(typeof(VerifiedAgencyFilter))]
    public class AuctionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuctionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private ApplicationUser GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _unitOfWork.ApplicationUser.Get(u => u.Id == userId, includeProperties: "Agency");
        }

        public IActionResult Index()
        {
            var user = GetCurrentUser();
            

            var model = _unitOfWork.Auction
            .GetAll(includeProperties: "InsuranceRequest")
            .Where(a => a.IsActive)
            .OrderByDescending(a => a.StartTime)
            .Select(a => new AuctionVM
            {
                Destination = a.InsuranceRequest.Destination,
                StartTime = a.StartTime,
                EndTime = a.EndTime,
            }).ToList();

            return View(model);
        }


        public IActionResult ClosedAuctions()
        {
            var user = GetCurrentUser();
      


            var model = _unitOfWork.Auction
                .GetAll(includeProperties: "InsuranceRequest")
                .Where(a => a.IsActive == false)
                .OrderByDescending(a => a.StartTime)
                .Select(a => new AuctionVM
                {
                    Destination = a.InsuranceRequest.Destination,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime,
                }).ToList();

            return View(model);




        }
        public IActionResult Statistics()
        {
            var user = GetCurrentUser();
            var agency = _unitOfWork.Agency.Get(a => a.Id == user.AgencyId);

            if (user == null || user.AgencyId == null)
                return NotFound();
          


            var offers = _unitOfWork.Offer
            .GetAll()
            .Where(o => o.AgencyId == user.AgencyId)
            .ToList();

            var wonOffers = offers.Where(o => o.isWinning == true).ToList();
            var gross = wonOffers.Sum(o => o.CurrentPrice);

            var model = new AgencyStatisticsVM
            {
                AgencyName = user.Agency?.Name ?? "N/A",
                Won = offers.Count(o => o.isWinning == true),
                Lost = offers.Count(o => o.isWinning == false),
                Pending = offers.Count(o => o.isWinning == null),
                GrossEarnings = gross,
                PlatformFee = gross * 0.10,
                TotalEarnings = gross * 0.90
            };

            return View(model);


        }

    }
}
