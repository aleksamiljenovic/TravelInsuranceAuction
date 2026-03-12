using System.Reflection.Metadata.Ecma335;

namespace TravelInsuranceAuction.ViewModels
{
    public class OfferVM
    {
        public string? AgencyName { get; set; }
        public double? InitialPrice { get; set; }
        public double? CurrentPrice { get; set; }
        public string? Conditions { get; set; }
        public int? AuctionId { get; set; }

    }
}
