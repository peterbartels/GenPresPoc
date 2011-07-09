using GenPres.Business.Domain.Prescription;

namespace GenPres.Business.Data.DataAccess.Repositories
{
    public interface IPrescriptionRepository
    {
        IPrescription[] GetPrescriptions();
        void SavePrescription(IPrescription prescription);
    }
}
