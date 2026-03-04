using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelInsuranceAuction.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string? StreetAddress { get; set; }
        public string? City { get; set; }


        public int? AgencyId { get; set; }
        [ForeignKey("AgencyId")]
        [ValidateNever]
        public Agency Agency { get; set; }
    }
}
