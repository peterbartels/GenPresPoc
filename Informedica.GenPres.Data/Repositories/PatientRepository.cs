using System;
using Informedica.GenPres.Business.Data.IRepositories;
using Informedica.GenPres.Business.Domain.Patients;

namespace Informedica.GenPres.Data.Repositories
{
    public class PatientRepository : NHibernateRepository<Patient, Guid>, IPatientRepository
    {
        public PatientRepository()
            : base(SessionManager.SessionFactory)
        {
            
        }

        public bool PatientExists(string patientId)
        {
            var foundPatient = FindSingle(x => x.Pid == patientId);
            return foundPatient != null;
        }

        public Patient GetPatientByPatientId(string pid)
        {
            return FindSingle(x => x.Pid == pid);
        }

        public void Save(Patient pat)
        {
            base.SaveOrUpdate(pat);
        }

    }
}
