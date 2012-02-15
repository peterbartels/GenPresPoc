using System.Collections;
using System.Collections.Generic;
using GenPres.Business.Domain.Patients;

namespace GenPres.Business.Data.IRepositories
{
    public interface IPatientRepository : IEnumerable<Patient>
    {
        Patient GetPatientByPatientId(string pid);
        bool PatientExists(string patientId);
        void Save(Patient pat);
    }
}
