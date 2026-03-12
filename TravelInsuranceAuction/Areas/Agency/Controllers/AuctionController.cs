using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public AuctionController(IUnitOfWork unitOfWork,ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public IActionResult Index()
        {

            var auctions = _context.Auctions.Include(a=>a.InsuranceRequest).ToList();

            var model = auctions.Select(a => new AuctionVM
            {
                Destination = a.InsuranceRequest.Destination,
                StartTime = a.StartTime,
                EndTime = a.EndTime,
            }).ToList();

            return View(model);

        }

        //#region API CALLS

        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    List<Auction> objAuctionList = _unitOfWork.Auction.GetAll().ToList();
        //    return Json(new { data = objAuctionList });
        //}

        //#endregion
    }
}
