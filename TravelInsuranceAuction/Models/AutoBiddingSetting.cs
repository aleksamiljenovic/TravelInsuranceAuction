using System.ComponentModel.DataAnnotations;

namespace TravelInsuranceAuction.Models
{
    public class AutoBiddingSetting
    {
        [Key]
        public int AutoBidId { get; set; }
        [Required]
        public double DefaultStartPrice { get; set; }
        [Required]
        public double DefaultMinPrice { get; set; }
        [Required]
        public double DecreasePercentage  { get; set; }

    }
}
