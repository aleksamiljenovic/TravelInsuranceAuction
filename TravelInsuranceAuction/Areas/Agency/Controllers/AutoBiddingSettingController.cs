using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using System.Security.Claims;
using TravelInsuranceAuction.Data;
using TravelInsuranceAuction.Models;
using TravelInsuranceAuction.Repository.IRepository;

namespace TravelInsuranceAuction.Areas.Agency.Controllers
{
    [Area("Agency")]
    public class AutoBiddingSettingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;


        public AutoBiddingSettingController(
            IUnitOfWork unitOfWork,
            ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            
        }


        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _unitOfWork.ApplicationUser.Get(u=>u.Id == userId);

            if (user == null || user.AgencyId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var settings = _unitOfWork.AutoBiddingSetting.Get(u=>u.AgencyId==user.AgencyId);

            //var settings = await _context.AutoBiddingSettings
            //    .FirstOrDefaultAsync(x => x.AgencyId == user.AgencyId);

            if (settings == null)
            {
                return RedirectToAction("Create");
            }

            return RedirectToAction("Edit", new { id = settings.Id });
        }
        public IActionResult Create()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
            var settings = _unitOfWork.AutoBiddingSetting.Get(u => u.AgencyId == user.AgencyId);

            if (settings == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Edit", new { id = settings.Id });
            }
            
        }

        [HttpPost]
        public IActionResult Create(AutoBiddingSetting obj)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = _unitOfWork.ApplicationUser
                .Get(u => u.Id == userId);

            obj.AgencyId = user.AgencyId;

            if (ModelState.IsValid)
            {
                _unitOfWork.AutoBiddingSetting.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Uspesno sacuvana podesavanja aukcije";
                return RedirectToAction("Index", "Home");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            AutoBiddingSetting? autoBiddingFromDb = _unitOfWork.AutoBiddingSetting.Get(u => u.Id == id);
            
            if (autoBiddingFromDb == null)
            {
                return NotFound();
            }
            return View(autoBiddingFromDb);
        }
        [HttpPost]
        public IActionResult Edit(AutoBiddingSetting obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.AutoBiddingSetting.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Uspesno izmenjena podesavanja aukcije";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
