using System;
using Informedica.GenPres.Business.Domain.Prescriptions;

namespace Informedica.GenPres.Business.Data.IRepositories
{
    public interface IPrescriptionRepository
    {
        Prescription[] GetPrescriptionsByPatientId(string patientId);
        Prescription GetPrescriptionById(Guid id);
        void SavePrescription(Prescription prescription, string patientId);
    }
}
