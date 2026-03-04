using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelInsuranceAuction.Models
{
    public class Auction
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTIme { get; set; }
        public bool IsActive { get; set; }
        public int RequestId { get; set; }
        [ForeignKey("RequestId")]
        public InsuranceRequest InsuranceRequest { get; set; }
        public ICollection<Offer> Offers { get; set; }
    }
}
