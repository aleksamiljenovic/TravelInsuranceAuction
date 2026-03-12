using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelInsuranceAuction.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        [Required(ErrorMessage = "Upišite vaše ime")]
        [DisplayName("Ime")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Upišite vaše prezime")]
        [DisplayName("Prezime")]
        public string LastName { get; set; }

        [DisplayName("Ulica")]
        public string? StreetAddress { get; set; }

        [DisplayName("Grad")]
        public string? City { get; set; }
        //public bool? IsVerified { get; set; }


        public int? AgencyId { get; set; }
        [ForeignKey("AgencyId")]
        [ValidateNever]
        public IncuranceAgency Agency { get; set; }
    }
}
