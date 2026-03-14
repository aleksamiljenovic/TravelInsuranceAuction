namespace TravelInsuranceAuction.ViewModels
{
    public class AgencyStatisticsVM
    {
        public string AgencyName { get; set; }
        public int Won { get; set; }
        public int Lost { get; set; }
        public int Pending { get; set; }
        public double TotalEarnings { get; set; }
        public double GrossEarnings { get; set; }
        public double PlatformFee { get; set; }
    }
}
