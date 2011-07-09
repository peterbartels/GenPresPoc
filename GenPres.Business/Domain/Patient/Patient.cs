using System;
using GenPres.Business.Data.DataAccess.Repositories;
using GenPres.Business.ServiceProvider;

namespace GenPres.Business.Domain.Patient
{
    public class Patient : IPatient
    {
        #region Private Fields

        private int _id;

        private string _lastName;

        private string _firstName;

        private string _pid;

        private int _logicalUnitId;

        private decimal _length;

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

        public string PID
        {
            get { return _pid; }
            set { _pid = value; }
        }

        public int LogicalUnitId
        {
            get { return _logicalUnitId; }
            set { _logicalUnitId = value; }
        }

        public decimal Length
        {
            get { return _length; }
            set { _length = value; }
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

        public bool IsNew { get; set; }
        public void OnCreate()
        {
            
        }

        public void OnNew()
        {
            
        }

        public void OnInitExisting()
        {
            
        }

        public void Save()
        {
            Repository.Save(this);
        }


        private static IPatientRepository Repository
        {
            get { return DalServiceProvider.Instance.Resolve<IPatientRepository>(); }
        }

        public static Patient NewPatient()
        {
            return new Patient();
        }

        public static IPatient GetPatientByPid(string Pid)
        {
            return Repository.GetByPid(Pid);
        }
    }
}
