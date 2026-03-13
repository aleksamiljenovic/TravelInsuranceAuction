namespace TravelInsuranceAuction.ViewModels
{
    public class AuctionOffersVM
    {
        public DateTime? AuctionStartTime { get; set; }
        public DateTime? AuctionEndTime { get; set; }
        public string? Destination { get; set; }
        public List<OfferVM> Offers { get; set; } = new();
        public int AuctionId { get; set; }//?
    }
}
