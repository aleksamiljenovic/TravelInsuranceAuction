using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using System.Security.Claims;
using TravelInsuranceAuction.Data;
using TravelInsuranceAuction.Models;
using TravelInsuranceAuction.Repository.IRepository;
using TravelInsuranceAuction.Utility;

namespace TravelInsuranceAuction.Areas.Agency.Controllers
{
    [Area("Agency")]
    [Authorize(Roles = SD.Role_Agency)]
    public class AutoBiddingSettingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public AutoBiddingSettingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }


        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

            if (user == null || user.AgencyId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (user.IsVerified == false)
            {
                return RedirectToAction("NotVerified", "Home");
            }
            else
            {
                var settings = _unitOfWork.AutoBiddingSetting.Get(u => u.AgencyId == user.AgencyId);



                if (settings == null)
                {
                    return RedirectToAction("Create");
                }

                return RedirectToAction("Edit", new { id = settings.Id });
            }


        }
        public IActionResult Create()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
            var settings = _unitOfWork.AutoBiddingSetting.Get(u => u.AgencyId == user.AgencyId);

            if (user.IsVerified == false)
            {
                return RedirectToAction("NotVerified", "Home");
            }
            else
            {
                if (settings == null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Edit", new { id = settings.Id });
                }
            }

            

        }

        [HttpPost]
        public IActionResult Create(AutoBiddingSetting obj)
        {
            var userId1 = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user1 = _unitOfWork.ApplicationUser.Get(u => u.Id == userId1);
            if (user1.IsVerified == false)
            {
                return RedirectToAction("NotVerified", "Home");
            }
            else
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
            
        }

        public IActionResult Edit(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

            if (user.IsVerified == false)
            {
                return RedirectToAction("NotVerified", "Home");
            }
            else
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                AutoBiddingSetting? autoBiddingFromDb = _unitOfWork.AutoBiddingSetting.Get(u => u.Id == id);

                if (autoBiddingFromDb != null)
                {
                    if (autoBiddingFromDb.AgencyId == user.AgencyId)
                    {
                        return View(autoBiddingFromDb);
                    }
                    else return Forbid();
                }
                else return NotFound();
            }

            

        }
        [HttpPost]
        public IActionResult Edit(AutoBiddingSetting obj)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

            if (user.IsVerified == false)
            {
                return RedirectToAction("NotVerified", "Home");
            }
            else
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
}
