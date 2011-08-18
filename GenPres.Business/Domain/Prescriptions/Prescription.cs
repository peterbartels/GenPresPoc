using System;
using System.Collections.Generic;
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

        public virtual IList<Dose> Doses { get; set; }

        public virtual UnitValue Frequency { get; set; }
        public virtual UnitValue Quantity { get; set; }
        public virtual UnitValue Total { get; set; }
        public virtual UnitValue Rate { get; set; }

        public virtual Patient Patient { get; set; }

        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }

        public virtual bool Solution { get; set; }
        public virtual bool Continuous { get; set; }
        public virtual bool Infusion { get; set; }
        public virtual bool OnRequest { get; set; }

        public virtual DateTime? CreationDate
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
            var prescription = new Prescription
            {
                Doses = new List<Dose> {Dose.NewDose()},
                Quantity = UnitValue.NewUnitValue(false),
                Frequency = UnitValue.NewUnitValue(false),
                Total = UnitValue.NewUnitValue(false)
            };
            prescription.Drug = Drug.NewDrug(prescription);
            return prescription;
        }
    }
}
