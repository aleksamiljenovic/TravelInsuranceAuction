using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using TravelInsuranceAuction.Repository.IRepository;

namespace TravelInsuranceAuction.Filters
{
    public class VerifiedAgencyFilter : IActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;

        public VerifiedAgencyFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return;

            var user = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
            if (user != null && user.IsVerified == false)
            {
                context.Result = new RedirectToActionResult("NotVerified", "Home", null);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
