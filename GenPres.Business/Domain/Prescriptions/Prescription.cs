using System;
using GenPres.Business.Domain.Patients;
using GenPres.Business.Domain.Units;

namespace GenPres.Business.Domain.Prescriptions
{
    public class Prescription 
    {   
        public Prescription()
        {
            //Drug = new DrugBo();
        }

        private DateTime? _creationDate;
        public virtual UnitValue Frequency { get; set; }
        public virtual UnitValue Quantity { get; set; }
        public virtual UnitValue Total { get; set; }

        public virtual Patient Patient { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual DateTime CreationDate
        {
            get {
                if (_creationDate == null)
                    _creationDate = DateTime.Now;
                return _creationDate.Value;
            }
            set { _creationDate = value; }
        }

        public virtual Drug Drug { get; set; }

        public virtual bool IsNew { get { return (Id.ToString() != ""); } }

        public virtual Guid Id
        {
            get; set;
        }

        public virtual string PID {get; set; }

        public static Prescription NewPrescription()
        {
            var prescription = new Prescription();
            prescription.Drug = Drug.NewDrug(prescription);
            prescription.Frequency = UnitValue.NewUnitValue(false);
            prescription.Quantity = UnitValue.NewUnitValue(false);
            prescription.Total = UnitValue.NewUnitValue(false);
            return prescription;
        }
    }
}
