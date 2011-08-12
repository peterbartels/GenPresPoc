using System;
using System.Collections.Generic;
using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.Business.Domain.Patients
{
    public class Patient
    {
       
        #region Public Properties

        public virtual Guid Id { get; set; }

        public virtual string LastName { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string FullName
        { 
            get { return FirstName + " " + LastName; }
        }

        public virtual string Pid { get; set; }

        public virtual int LogicalUnitId { get; set; }

        public virtual decimal Height { get; set; }

        public virtual decimal Weight { get; set; }

        public virtual string Gender { get; set; }

        public virtual string Unit { get; set; }

        public virtual string Bed { get; set; }

        public virtual string Birthdate { get; set; }

        public virtual string Age { get; set; }

        public virtual DateTime RegisterDate { get; set; }

        public virtual int DaysRegistered { get; set; }

        public virtual List<Prescription> Prescriptions { get; set; }

        #endregion

        public virtual bool IsNew { get { return (Id == Guid.Empty); } }

        public virtual void Save()
        {
            Repository.Save(this);
        }

        private static IPatientRepository _patientRepository;

        private static IPatientRepository Repository
        {
            get
            {
                if (_patientRepository == null) _patientRepository = StructureMap.ObjectFactory.GetInstance<IPatientRepository>();
                return _patientRepository;
            }
        }

        private static readonly IPdsmRepository PdmsPatientRepository =
            StructureMap.ObjectFactory.GetInstance<IPdsmRepository>();

        private static IPdsmRepository PdmsRepository
        {
            get { return PdmsPatientRepository; }
        }

        public static Patient NewPatient()
        {
            return new Patient();
        }

        public static Patient GetPatientByPid(string Pid)
        {
            return Repository.GetByPid(Pid);
        }

        public static bool InsertFromPdms(string pid)
        {
            if (!Repository.PatientExists(pid))
            {
                var patient = PdmsRepository.GetPatientByPid(pid);
                //Repository.Save(patient);
                //Repository.Submit();
                return true;
            }
            return false;
        }
    }
}
