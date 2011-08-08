using GenPres.Business.Domain.Prescriptions;

namespace GenPres.Business.Data.IRepositories
{
    public interface IPrescriptionRepository
    {
        IPrescription[] GetPrescriptions(string patientId);
        IPrescription GetPrescriptionById(int id);
        void SavePrescription(IPrescription prescription, string patientId);
    }
}
