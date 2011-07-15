using GenPres.Business.Domain.PrescriptionDomain;

namespace GenPres.Business.Data.DataAccess.Repositories
{
    public interface IPrescriptionRepository
    {
        Prescription[] GetPrescriptions(string patientId);
        Prescription GetPrescriptionById(int id);
        void SavePrescription(Prescription prescription, string patientId);
    }
}
