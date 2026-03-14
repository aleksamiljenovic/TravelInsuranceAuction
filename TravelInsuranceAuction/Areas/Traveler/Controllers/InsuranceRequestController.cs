using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using System.Security.Claims;
using TravelInsuranceAuction.Data;
using TravelInsuranceAuction.Hubs;
using TravelInsuranceAuction.Models;
using TravelInsuranceAuction.Repository.IRepository;
using TravelInsuranceAuction.Utility;
using TravelInsuranceAuction.ViewModels;

namespace TravelInsuranceAuction.Areas.Traveler.Controllers
{
    [Area("Traveler")]
    [Authorize(Roles = SD.Role_Traveler)]
    public class InsuranceRequestController : Controller
    {


        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<PriceHub> _hubContext;

        public InsuranceRequestController(IUnitOfWork unitOfWork, IHubContext<PriceHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }



        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<InsuranceRequest> objInsuranceRequest = _unitOfWork.InsuranceRequest
                .GetAll()
                .Where(u => u.UserId == userId &&
                       _unitOfWork.Auction.GetAll()
                       .Any(a => a.RequestId == u.Id && a.IsActive)).OrderByDescending(u => u.createdAt)
                .ToList();

            return View(objInsuranceRequest);

        }

        public IActionResult ClosedAuctions()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<InsuranceRequest> objInsuranceRequest = _unitOfWork.InsuranceRequest
                .GetAll()
                .Where(u => u.UserId == userId &&
                       _unitOfWork.Auction.GetAll()
                       .Any(a => a.RequestId == u.Id && a.IsActive==false)).OrderByDescending(u => u.createdAt)
                .ToList();

            return View(objInsuranceRequest);
        }


        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(InsuranceRequest obj)
        {
            if (obj.EndDate <= obj.StartDate)
            {
                ModelState.AddModelError("enddate", "Izaberite validan datum povratka.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            if (ModelState.IsValid && userId != null)
            {
                obj.UserId = userId;
                _unitOfWork.InsuranceRequest.Add(obj);
                _unitOfWork.Save();

                Auction auction = new Auction();
                auction.StartTime = DateTime.Now;
                auction.EndTime = auction.StartTime.AddHours(6);
                auction.IsActive = true;
                auction.RequestId = obj.Id;

                List<IncuranceAgency> agenciesList = _unitOfWork.Agency.GetAll().ToList();

                _unitOfWork.Auction.Add(auction);
                _unitOfWork.Save();
                var auctionId = auction.Id;
                foreach (var agency in agenciesList)
                {
                    var bid = _unitOfWork.AutoBiddingSetting.Get(u => u.AgencyId == agency.Id);
                    if (bid != null)
                    {
                        Offer offer = new Offer();
                        offer.InitialPrice = bid.DefaultStartPrice;
                        offer.CurrentPrice = bid.DefaultStartPrice - (bid.DefaultStartPrice * bid.PriceDecrease / 100);
                        offer.Conditions = bid.SpecialConditions;
                        offer.AgencyId = agency.Id;
                        offer.AuctionId = auctionId;
                        _unitOfWork.Offer.Add(offer);
                        _unitOfWork.Save();
                    }

                }

                TempData["success"] = "Licitacija uspesno kreirana";
                return RedirectToAction("Index");

            }



            return View();
        }

        public IActionResult Show(int id)
        {

            var request = _unitOfWork.InsuranceRequest.Get(r => r.Id == id);

            if (request == null)
                return NotFound();


            var auction = _unitOfWork.Auction.Get(u => u.RequestId == request.Id);

            if (auction == null)
            {
                return NotFound();
            }
            else
            {

                var offersList = _unitOfWork.Offer.GetAll()
                     .Where(u => u.AuctionId == auction.Id)
                     .ToList();

                //var offers = _unitOfWork.Offer.Get(u => u.AuctionId == auction.Id);
                //var agency = _unitOfWork.Agency.Get(u => u.Id == offers.AgencyId);

                var offersVM = offersList.Select(o =>
                {
                    var agency = _unitOfWork.Agency.Get(a => a.Id == o.AgencyId);
                    var auction = _unitOfWork.Auction.Get(a => a.Id == o.AuctionId);
                    return new OfferVM
                    {
                        Id = o.Id,
                        AgencyName = agency != null ? agency.Name : "Nepoznata agencija",
                        InitialPrice = o.InitialPrice,
                        CurrentPrice = o.CurrentPrice,
                        Conditions = o.Conditions,
                        AuctionId = o.AuctionId

                    };
                }).ToList();

                //var offersVM = offersList.Select(o => new OfferVM
                //{
                //    AgencyName = o.Agency != null ? o.Agency.Name : "Nepoznata agencija",
                //    InitialPrice = o.InitialPrice,
                //    CurrentPrice = o.CurrentPrice,
                //    Conditions = o.Conditions,
                //    AuctionId = o.AuctionId
                //}).ToList();

                var model = new AuctionOffersVM
                {
                    AuctionStartTime = auction?.StartTime,
                    AuctionEndTime = auction?.EndTime,
                    Destination = request?.Destination,
                    Offers = offersVM
                };

                return View(model);
            }

        }

        [HttpPost]
        public async Task<IActionResult> SelectOffer(int offerId)
        {
            var offer = _unitOfWork.Offer.Get(o => o.Id == offerId);

            if (offer == null)
                return NotFound();

            var auction = _unitOfWork.Auction.Get(a => a.Id == offer.AuctionId);

            if (auction == null)
                return NotFound();

            //if (!auction.IsActive)
            //{
            //    return RedirectToAction("Bill", "Payment", new { offerId = offer.Id });
            //}

            var allOffers = _unitOfWork.Offer.GetAll().Where(o => o.AuctionId == auction.Id).ToList();
            foreach (var o in allOffers)
            {
                o.isWinning = false;
                _unitOfWork.Offer.Update(o);
            }

            offer.isWinning = true;
            auction.IsActive = false;

            _unitOfWork.Auction.Update(auction);
            _unitOfWork.Save();

            // SignalR obaveštava sve
            await _hubContext.Clients.Group($"auction-{auction.Id}")
                .SendAsync("AuctionFinished", offer.Id);

            return RedirectToAction("Payment", "Payment", new { offerId = offer.Id });
        }


    }
}
