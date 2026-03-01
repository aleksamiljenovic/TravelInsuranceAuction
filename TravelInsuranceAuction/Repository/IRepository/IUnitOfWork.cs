namespace TravelInsuranceAuction.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IInsuranceRequestRepository InsuranceRequest { get; }

        void Save();
    }
}
