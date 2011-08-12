using System;
using GenPres.Business.Data.IRepositories;

namespace GenPres.Business.Domain.Patients
{
    public class Patient : IPatient
    {
        #region Private Fields

        private int _id;

        private string _lastName;

        private string _firstName;

        private string _pid;

        private int _logicalUnitId;

        private decimal _height;

        private decimal _weight;

        private string _gender;

        private string _unit;

        private string _bed;

        private string _birthdate;

        private string _age;

        private DateTime _registerDate;

        private int _daysRegistered;

        #endregion 

        #region Public Properties

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string FullName { 
            get { return FirstName + " " + LastName; }
        }

        public string Pid
        {
            get { return _pid; }
            set { _pid = value; }
        }

        public int LogicalUnitId
        {
            get { return _logicalUnitId; }
            set { _logicalUnitId = value; }
        }

        public decimal Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public decimal Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        public string Bed
        {
            get { return _bed; }
            set { _bed = value; }
        }

        public string Birthdate
        {
            get { return _birthdate; }
            set { _birthdate = value; }
        }

        public string Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public DateTime RegisterDate
        {
            get { return _registerDate; }
            set { _registerDate = value; }
        }

        public int DaysRegistered
        {
            get { return _daysRegistered; }
            set { _daysRegistered = value; }
        }

        #endregion

        public bool IsNew { get { return (Id == 0); } }

        public void Save()
        {
            SqlRepository.Save(this);
            SqlRepository.Submit();
        }

        private static readonly IPatientSqlRepository PatientSqlRepository = 
            StructureMap.ObjectFactory.GetInstance<IPatientSqlRepository>();

        private static IPatientSqlRepository SqlRepository
        {
            get { return PatientSqlRepository; }
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

        public static IPatient GetPatientByPid(string Pid)
        {
            return SqlRepository.GetByPid(Pid);
        }

        public static bool InsertFromPdms(string pid)
        {
            if (!SqlRepository.PatientExists(pid))
            {
                var patient = PdmsRepository.GetPatientByPid(pid);
                SqlRepository.Save(patient);
                SqlRepository.Submit();
                return true;
            }
            return false;
        }
    }
}
