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
            : base(SessionFactoryManager.SessionFactory)
        {
            
        }

        public Patient FindOrCreatePatient(string patientId)
        {
            
            return Patient.NewPatient();
        }

        public bool PatientExists(string patientId)
        {
            var foundPatient = FindSingle(x => x.Pid == patientId);
            return foundPatient != null;
        }

        public Patient GetByPid(string pid)
        {
            return Patient.NewPatient();
            //return newPatient;
        }


        public void Save(Patient pat)
        {
            base.Add(pat);
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public IPatient Save(IPatient businessObject)
        {
            throw new NotImplementedException();
        }
    }
}
