using System.ComponentModel.DataAnnotations;

namespace TravelInsuranceAuction.Models
{
    public class IncuranceAgency
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
    }
}
