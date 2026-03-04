namespace TravelInsuranceAuction.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IInsuranceRequestRepository InsuranceRequest { get; }
        IAgencyRepository Agency { get; }

        void Save();
    }
}
