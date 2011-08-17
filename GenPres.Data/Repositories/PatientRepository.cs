using System;
using System.Collections.Generic;
using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Patients;
using NHibernate;
using NHibernate.Linq;
using System.Linq;

namespace GenPres.Data.Repositories
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

        public Patient GetByPid(string pid)
        {
            return FindSingle(x => x.Pid == pid);
        }

        public void Save(Patient pat)
        {
            base.SaveOrUpdate(pat);
        }

        public int Count()
        {
            throw new NotImplementedException();
        }
    }
}
