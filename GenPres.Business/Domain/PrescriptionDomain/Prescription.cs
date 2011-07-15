using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Data.DataAccess.Repositories;
using GenPres.Business.Domain.PrescriptionDomain;
using GenPres.Business.ServiceProvider;

namespace GenPres.Business.Domain.PrescriptionDomain
{
    public class Prescription : IPrescription
    {
        #region Private Fields

        private DateTime _startDate;

        private DateTime _endDate;

        private DateTime _creationDate;

        private Drug _drug;

        private string _pid;

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

        public Drug Drug
        {
            get { return _drug; }
            set { _drug = value; }
        }

        public string PID
        {
            get { return _pid; }
            set { _pid = value; }
        }

        #endregion

        private static IPrescriptionRepository Repository
        {
            get { return DalServiceProvider.Instance.Resolve<IPrescriptionRepository>(); }
        }

        public static Prescription NewPrescription()
        {
            return ObjectFactory.New<Prescription>();
        }

        public static IPrescription[] GetPrescriptions(string patientId)
        {
            return Repository.GetPrescriptions(patientId);
        }


        public static IPrescription GetPrescriptionById(int id)
        {
            return Repository.GetPrescriptionById(id);
        }


        public bool IsNew { get; set; }

        public void OnCreate()
        {
            
        }

        public void OnNew()
        {
            CreationDate = DateTime.Now;
            StartDate = DateTime.Now;
            Drug = Drug.NewDrug();
        }

        public void OnInitExisting()
        {
            
        }

        public void Save()
        {
            
        }

        public void Save(string patientId)
        {
            Repository.SavePrescription(this, patientId);
        }

        public int Id {get;set;}
    }
}
