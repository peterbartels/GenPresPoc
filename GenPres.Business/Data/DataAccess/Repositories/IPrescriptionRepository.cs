using GenPres.Business.Domain.PrescriptionDomain;

namespace GenPres.Business.Data.DataAccess.Repositories
{
    public interface IPrescriptionRepository
    {
        IPrescription[] GetPrescriptions(string patientId);
        IPrescription GetPrescriptionById(int id);
        void SavePrescription(IPrescription prescription, string patientId);
    }
}
