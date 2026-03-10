using System.ComponentModel.DataAnnotations;

namespace TravelInsuranceAuction.Models
{
    public class Offer
    {
        [Key]
        public int Id { get; set; }
        public double InitialPrice { get; set; }
        public double CurrentPrice { get; set; }
        public string? Conditions { get; set; }
    }
}
