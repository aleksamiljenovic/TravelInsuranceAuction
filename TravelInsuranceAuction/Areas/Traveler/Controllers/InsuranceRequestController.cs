using Azure.Core;
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
using TravelInsuranceAuction.Services;
using TravelInsuranceAuction.Utility;
using TravelInsuranceAuction.ViewModels;

namespace TravelInsuranceAuction.Areas.Traveler.Controllers
{
    [Area("Traveler")]
    [Authorize(Roles = SD.Role_Traveler)]
    public class InsuranceRequestController : Controller
    {

        private readonly InsuranceRequestService _service;

        public InsuranceRequestController(InsuranceRequestService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return NotFound();

            return View(_service.GetActiveUser(userId));

        }

        public IActionResult ClosedAuctions()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return NotFound();

            return View(_service.GetClosedByUser(userId));
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(InsuranceRequest obj)
        {
            if (obj.EndDate <= obj.StartDate)
                ModelState.AddModelError("enddate", "Izaberite validan datum povratka.");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid && userId != null)
            {
                await _service.CreateAuction(obj, userId);
                TempData["success"] = "Aukcija uspesno kreirana";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Show(int id)
        {
            var model = _service.GetAuctionOffers(id);
            if (model == null) return NotFound();
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> SelectOffer(int offerId)
        {
            await _service.SelectOffer(offerId);
            var offer = _service.SelectOffer(offerId);
            return RedirectToAction("Payment", "Payment", new { offerId });
        }

        [HttpPost]
        public async Task<IActionResult> CancelAuction(int auctionId)
        {
            await _service.CancelAuction(auctionId);
            return RedirectToAction("Index");
        }

    }
}
