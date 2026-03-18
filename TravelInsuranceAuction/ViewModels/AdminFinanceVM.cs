namespace TravelInsuranceAuction.ViewModels
{
    public class AdminFinanceVM
    {
        public double TotalGross { get; set; }
        public double TotalPlatformFee { get; set; }
        public int TotalAuctions { get; set; }
        public List<AgencyFinanceVM> ByAgency { get; set; }
    }
}
