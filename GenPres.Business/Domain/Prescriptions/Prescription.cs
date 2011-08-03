using System;
using GenPres.Business.Data.DataAccess.Repositories;
using GenPres.Business.Domain.Units;

namespace GenPres.Business.Domain.Prescriptions
{
    public class Prescription : IPrescription
    {
        #region Private Fields

        private int _id;

        private DateTime _startDate;

        private DateTime _endDate;

        private DateTime _creationDate;

        private IDrug _drug;

        private string _pid;

        private UnitValue _frequency;

        private UnitValue _quantity;

        private UnitValue _total;

        private UnitValue _rate;

        #endregion

        #region Public Properties

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public DateTime CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }

        public IDrug Drug
        {
            get { return _drug; }
            set { _drug = value; }
        }

        public string PID
        {
            get { return _pid; }
            set { _pid = value; }
        }

        public UnitValue Frequency
        {
            get { return _frequency; }
            set { _frequency = value; }
        }

        public UnitValue Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public UnitValue Total
        {
            get { return _total; }
            set { _total = value; }
        }

        public UnitValue Rate
        {
            get { return _rate; }
            set { _rate = value; }
        }

        #endregion

        #region Operations

        private static IPrescriptionRepository Repository
        {
            get
            {
                return StructureMap.ObjectFactory.GetInstance<IPrescriptionRepository>();
            }
        }

        public static IPrescription NewPrescription()
        {
            return ObjectFactory.New<IPrescription>();
        }

        public static IPrescription[] GetPrescriptions(string patientId)
        {
            return Repository.GetPrescriptions(patientId);
        }


        public static IPrescription GetPrescriptionById(int id)
        {
            return Repository.GetPrescriptionById(id);
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        #endregion

        public bool IsNew { get { return (Id == 0); } }

        public void OnCreate() {}
        public void OnInitExisting() { }
        
        public void OnNew()
        {
            CreationDate = DateTime.Now;
            StartDate = DateTime.Now;
            
            Drug = Prescriptions.Drug.NewDrug();

            Frequency = new UnitValue();
            Quantity = new UnitValue();
            Total = new UnitValue();
        }

        public void Save(string patientId)
        {
            Repository.SavePrescription(this, patientId);
        }

        
    }
}
