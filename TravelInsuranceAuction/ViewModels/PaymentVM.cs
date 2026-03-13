namespace TravelInsuranceAuction.ViewModels
{
    public class PaymentVM
    {
        public int OfferId { get; set; }
        public string AgencyName { get; set; }
        public double Price { get; set; }
        public string Destination { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}
