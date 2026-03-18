using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelInsuranceAuction.Models
{
    public class Offer
    {
        [Key]
        public int Id { get; set; }
        public double? InitialPrice { get; set; }
        public double CurrentPrice { get; set; } 
        public string? Conditions { get; set; }
        public DateTime? LastPriceDecrease { get; set; } 
        public bool? isWinning { get; set; }
        public int? AgencyId { get; set; }
        [ForeignKey("AgencyId")]
        [ValidateNever]
        public IncuranceAgency? Agency { get; set; }
        public int? AuctionId { get; set; }
        
        [ForeignKey("AuctionId")]
        [ValidateNever]
        public Auction? Auction { get; set; }
    }
}
