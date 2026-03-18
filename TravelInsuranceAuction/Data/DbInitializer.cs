using Microsoft.AspNetCore.Identity;
using TravelInsuranceAuction.Models;
using TravelInsuranceAuction.Utility;

namespace TravelInsuranceAuction.Data
{
    public class DbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            // Uvek kreiraj role ako ne postoje
            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();

            if (!_roleManager.RoleExistsAsync(SD.Role_Traveler).GetAwaiter().GetResult())
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Traveler)).GetAwaiter().GetResult();

            if (!_roleManager.RoleExistsAsync(SD.Role_Agency).GetAwaiter().GetResult())
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Agency)).GetAwaiter().GetResult();

            // Uvek kreiraj admina ako ne postoji
            if (_userManager.FindByEmailAsync("admin@gmail.com").GetAwaiter().GetResult() == null ||
                _userManager.FindByEmailAsync("traveler1@gmail.com").GetAwaiter().GetResult() == null ||
                _userManager.FindByEmailAsync("traveler2@gmail.com").GetAwaiter().GetResult() == null ||
                _userManager.FindByEmailAsync("agency1@gmail.com").GetAwaiter().GetResult() == null ||
                _userManager.FindByEmailAsync("agency2@gmail.com").GetAwaiter().GetResult() == null ||
                _userManager.FindByEmailAsync("agency3@gmail.com").GetAwaiter().GetResult() == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                    IsVerified = true,
                };
                var traveler1 = new ApplicationUser
                {
                    UserName = "traveler1@gmail.com",
                    Email = "traveler1@gmail.com",
                    EmailConfirmed = true,
                    IsVerified = true
                };
                var traveler2 = new ApplicationUser
                {
                    UserName = "traveler2@gmail.com",
                    Email = "traveler2@gmail.com",
                    EmailConfirmed = true,
                    IsVerified = true
                };
                var agency1 = new ApplicationUser
                {
                    UserName = "agency1@gmail.com",
                    Email = "agency1@gmail.com",
                    EmailConfirmed = true,
                    IsVerified = false,
                    AgencyId = 1
                };
                var agency2 = new ApplicationUser
                {
                    UserName = "agency2@gmail.com",
                    Email = "agency2@gmail.com",
                    EmailConfirmed = true,
                    IsVerified = false,
                    AgencyId = 2
                };
                var agency3 = new ApplicationUser
                {
                    UserName = "agency3@gmail.com",
                    Email = "agency3@gmail.com",
                    EmailConfirmed = true,
                    IsVerified = false,
                    AgencyId = 3
                };

                var resultAdmin = _userManager.CreateAsync(admin, "$Ifra123").GetAwaiter().GetResult();
                var resultTraveler1 = _userManager.CreateAsync(traveler1, "$Ifra123").GetAwaiter().GetResult();
                var resultTraveler2 = _userManager.CreateAsync(traveler2, "$Ifra123").GetAwaiter().GetResult();
                var resultAgency1 = _userManager.CreateAsync(agency1, "$Ifra123").GetAwaiter().GetResult();
                var resultAgency2 = _userManager.CreateAsync(agency2, "$Ifra123").GetAwaiter().GetResult();
                var resultAgency3 = _userManager.CreateAsync(agency3, "$Ifra123").GetAwaiter().GetResult();

                if (resultAdmin.Succeeded ||
                    resultTraveler1.Succeeded ||
                    resultTraveler2.Succeeded ||
                    resultAgency1.Succeeded ||
                    resultAgency2.Succeeded ||
                    resultAgency3.Succeeded)
                    _userManager.AddToRoleAsync(admin, SD.Role_Admin).GetAwaiter().GetResult();
                    _userManager.AddToRoleAsync(traveler1, SD.Role_Traveler).GetAwaiter().GetResult();
                    _userManager.AddToRoleAsync(traveler2, SD.Role_Traveler).GetAwaiter().GetResult();
                    _userManager.AddToRoleAsync(agency1, SD.Role_Agency).GetAwaiter().GetResult();
                    _userManager.AddToRoleAsync(agency2, SD.Role_Agency).GetAwaiter().GetResult();
                    _userManager.AddToRoleAsync(agency3, SD.Role_Agency).GetAwaiter().GetResult();
            }
        }
    }
}
