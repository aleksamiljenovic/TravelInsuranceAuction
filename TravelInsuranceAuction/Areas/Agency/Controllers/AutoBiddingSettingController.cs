using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using System.Security.Claims;
using TravelInsuranceAuction.Data;
using TravelInsuranceAuction.Filters;
using TravelInsuranceAuction.Models;
using TravelInsuranceAuction.Repository.IRepository;
using TravelInsuranceAuction.Utility;

namespace TravelInsuranceAuction.Areas.Agency.Controllers
{
    [Area("Agency")]
    [Authorize(Roles = SD.Role_Agency)]
    [ServiceFilter(typeof(VerifiedAgencyFilter))]
    public class AutoBiddingSettingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public AutoBiddingSettingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private ApplicationUser GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
        }

        public IActionResult Index()
        {
            var user = GetCurrentUser();

            if (user == null || user.AgencyId == null)
                return RedirectToAction("Index", "Home");

            var settings = _unitOfWork.AutoBiddingSetting.Get(u => u.AgencyId == user.AgencyId);


            if (settings == null)
                return RedirectToAction("Create");

            return RedirectToAction("Edit", new { id = settings.Id });
        }

        public IActionResult Create()
        {
            var user = GetCurrentUser();
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
            var user = GetCurrentUser();
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
                return NotFound();

            var user = GetCurrentUser();
            var settings = _unitOfWork.AutoBiddingSetting.Get(u => u.Id == id);

            if (settings == null)
                return NotFound();

            if (settings.AgencyId != user.AgencyId)
                return Forbid();

            return View(settings);


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
