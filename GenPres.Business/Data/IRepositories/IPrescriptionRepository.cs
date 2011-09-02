using System;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.Business.Data.IRepositories
{
    public interface IPrescriptionRepository
    {
        Prescription[] GetPrescriptionsByPatientId(string patientId);
        Prescription GetPrescriptionById(Guid id);
        void SavePrescription(Prescription prescription, string patientId);
    }
}
