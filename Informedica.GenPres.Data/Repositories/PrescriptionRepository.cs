using System;
using System.Linq;
using Informedica.GenPres.Business.Data.IRepositories;
using Informedica.GenPres.Business.Domain.Patients;
using Informedica.GenPres.Business.Domain.Prescriptions;
using NHibernate.Linq;

namespace Informedica.GenPres.Data.Repositories
{
    
    public class PrescriptionRepository : NHibernateRepository<Prescription, Guid>, IPrescriptionRepository
    {
        public PrescriptionRepository() : base(SessionManager.SessionFactory){}

        public Prescription[] GetPrescriptionsByPatientId(string patientId)
        {
            return GetByPatientId(patientId);
        }

        public Prescription GetPrescriptionById(Guid id)
        {
            return GetById(id);
        }

        private Prescription GetById(Guid id)
        {
            return Session.Query<Prescription>().SingleOrDefault(x => x.Id == id);
        }


        private Prescription[] GetByPatientId(string patientId)
        {
            var prescriptions = Session.Query<Prescription>().Where(x => x.Patient.Pid == patientId);
            return prescriptions.ToArray();
        }


        public void SavePrescription(Prescription prescription, string patientId)
        {
            var pr = new PatientRepository();
            var pat = pr.GetPatientByPatientId(patientId);
            if(pat == null)
            {
                pat = Patient.NewPatient();
                pat.Pid = patientId;
            }
            
            pat.Prescriptions.Add(prescription);
            pr.SaveOrUpdate(pat);
        }
    }
}
