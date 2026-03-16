using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TravelInsuranceAuction.Models;
using TravelInsuranceAuction.Repository.IRepository;

namespace TravelInsuranceAuction.Areas.Agency.Controllers
{
    [Area("Agency")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _unitOfWork.ApplicationUser.Get(u=>u.Id==userId);
            if (user.IsVerified == false)
            {
                return RedirectToAction("NotVerified", "Home");
            }
            else
            {
                return RedirectToAction("Statistics", "Auction");
            }
        }
        public IActionResult NotVerified()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
