using GenPres.Business.Domain.Prescription;

namespace GenPres.Business.Data.DataAccess.Repository
{
    public interface IPrescriptionRepository
    {
        IPrescription[] GetPrescriptions();
        void SavePrescription(IPrescription prescription);
    }
}
