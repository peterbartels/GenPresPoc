using System;
using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Units;
using GenPres.Business.WebService;

namespace GenPres.Business.Domain.Prescriptions
{
    public class Prescription : IPrescription
    {
        #region Public Properties

        public virtual bool IsNew { get { return (Id == 0); } }

        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual IDrug Drug { get; set; }
        public virtual string PID { get; set; }
        public virtual UnitValue Frequency { get; set; }
        public virtual UnitValue Quantity { get; set; }
        public virtual UnitValue Total { get; set; }
        public virtual UnitValue Rate { get; set; }

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
            return ObjectCreator.New<IPrescription>();
        }

        public static IPrescription[] GetPrescriptions(string patientId)
        {
            return Repository.GetPrescriptions(patientId);
        }


        public static IPrescription GetPrescriptionById(int id)
        {
            return Repository.GetPrescriptionById(id);
        }

        public virtual int Id { get; set; }

        #endregion

        public Prescription()
        {
            CreationDate = DateTime.Now;
            StartDate = DateTime.Now;

            Drug = Prescriptions.Drug.NewDrug();

            Frequency = new UnitValue();
            Quantity = new UnitValue();
            Total = new UnitValue();
        }

        public virtual void Save(string patientId)
        {
            Repository.SavePrescription(this, patientId);
        }
    }
}
