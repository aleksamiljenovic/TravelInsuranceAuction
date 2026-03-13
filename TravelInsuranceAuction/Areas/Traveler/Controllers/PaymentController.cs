using Microsoft.AspNetCore.Mvc;
using TravelInsuranceAuction.Repository.IRepository;
using TravelInsuranceAuction.ViewModels;

namespace TravelInsuranceAuction.Areas.Traveler.Controllers
{
    [Area("Traveler")]
    public class PaymentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Payment(int offerId)
        {
            var offer = _unitOfWork.Offer.Get(o => o.Id == offerId);
            var auction = _unitOfWork.Auction.Get(a => a.Id == offer.AuctionId);
            var request = _unitOfWork.InsuranceRequest.Get(r => r.Id == auction.RequestId);
            var agency = _unitOfWork.Agency.Get(a => a.Id == offer.AgencyId);

            PaymentVM model = new PaymentVM
            {
                OfferId = offer.Id,
                AgencyName = agency.Name,
                Price = offer.CurrentPrice,
                Destination = request.Destination,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult ConfirmPayment(int offerId)
        {
            var offer = _unitOfWork.Offer.Get(o => o.Id == offerId);

            if (offer == null)
                return NotFound();

            // ovde možeš kasnije dodati PaymentStatus u bazu

            return RedirectToAction("Success");
        }
    }
}
