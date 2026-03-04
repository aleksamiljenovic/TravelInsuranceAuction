using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelInsuranceAuction.Data;
using TravelInsuranceAuction.Models;
using TravelInsuranceAuction.Repository.IRepository;
using TravelInsuranceAuction.Utility;

namespace TravelInsuranceAuction.Areas.Traveler.Controllers
{
    [Area("Traveler")]
    [Authorize(Roles = SD.Role_Traveler)]
    public class InsuranceRequestController : Controller
    {
        

        private readonly IUnitOfWork _unitOfWork;

        public InsuranceRequestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        
        public IActionResult Index()
        {
            List<InsuranceRequest> objInsuranceRequest = _unitOfWork.InsuranceRequest.GetAll().ToList();
            return View(objInsuranceRequest);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(InsuranceRequest obj)
        {
            if(obj.EndDate <= obj.StartDate)
            {
                ModelState.AddModelError("enddate", "Izaberite validan datum povratka.");
            }
            
            if (ModelState.IsValid)
            {
                _unitOfWork.InsuranceRequest.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Licitacija uspesno kreirana";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Show()
        {
            return View();
        }
    }
}
