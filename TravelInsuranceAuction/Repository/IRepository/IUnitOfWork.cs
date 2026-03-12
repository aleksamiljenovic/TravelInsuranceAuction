namespace TravelInsuranceAuction.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IInsuranceRequestRepository InsuranceRequest { get; }
        IAgencyRepository Agency { get; }
        IAutoBiddingSettingRepository AutoBiddingSetting { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IAuctionRepository Auction { get; }
        IOfferRepository Offer { get; }

        void Save();
    }
}
