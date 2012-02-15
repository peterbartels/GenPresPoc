using System.Collections.Generic;
using Informedica.GenPres.Business.Domain.Patients;

namespace Informedica.GenPres.Business.Data.IRepositories
{
    public interface IPatientRepository : IEnumerable<Patient>
    {
        Patient GetPatientByPatientId(string pid);
        bool PatientExists(string patientId);
        void Save(Patient pat);
    }
}
