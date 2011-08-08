using System;
using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Units;
using GenPres.Business.WebService;

namespace GenPres.Business.Domain.Prescriptions
{
    public class Prescription : IPrescription
    {
        #region Public Properties

        public bool IsNew { get { return (Id == 0); } }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreationDate { get; set; }
        public IDrug Drug { get; set; }
        public string PID { get; set; }
        public UnitValue Frequency { get; set; }
        public UnitValue Quantity { get; set; }
        public UnitValue Total { get; set; }
        public UnitValue Rate { get; set; }

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

        public int Id { get; set; }

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

        public void Save(string patientId)
        {
            Repository.SavePrescription(this, patientId);
        }
    }
}
