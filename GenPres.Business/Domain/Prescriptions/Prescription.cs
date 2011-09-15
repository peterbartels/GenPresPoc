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
        public virtual UnitValue Duration { get; set; }

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

        private static List<string> _volumeList = new List<string>() { "ml", "cl", "dl", "l" };

        public virtual bool AdminVolume
        {
            get
            {
                return _volumeList.Contains(Quantity.Unit);
            }
        }

        public virtual bool DoseVolume
        {
            get
            {
                return _volumeList.Contains(Doses[0].Quantity.Unit);
            }
        }

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

        public virtual UnitValue PatientWeight { get; set; }

        public static Prescription NewPrescription()
        {
            var prescription = new Prescription
            {
                Doses = new List<Dose> {Dose.NewDose()},
                Quantity = UnitValue.NewUnitValue(false),
                Frequency = UnitValue.NewUnitValue(false),
                Total = UnitValue.NewUnitValue(false),
                Rate = UnitValue.NewUnitValue(false),
                Duration = UnitValue.NewUnitValue(false),
                PatientWeight = UnitValue.NewUnitValue(true)
            };
            prescription.Frequency.Time = "dag";
            prescription.Total.Time = "dag";
            prescription.Rate.Time = "uur";
            prescription.Duration.Time = "uur";
            prescription.PatientWeight.Unit = "kg";

            prescription.Drug = Drug.NewDrug(prescription);
            return prescription;
        }

        public virtual void SetDefaultUnits(string substanceUnit, string componentUnit)
        {
            Drug.Components[0].Substances[0].ComponentConcentration.Unit = substanceUnit;
            Drug.Components[0].Substances[0].ComponentConcentration.Total = componentUnit;
            Drug.Components[0].Substances[0].DrugConcentration.Unit = substanceUnit;
            Drug.Components[0].Substances[0].DrugConcentration.Total = componentUnit;
            Drug.Components[0].Substances[0].Quantity.Unit = substanceUnit;

            Drug.Components[0].DrugConcentration.Unit = componentUnit;
            Drug.Components[0].DrugConcentration.Total = componentUnit;
            Drug.Components[0].Quantity.Unit = componentUnit;

            Drug.Quantity.Unit = componentUnit;

            Quantity.Unit = componentUnit;
            Total.Unit = componentUnit;
            Rate.Unit = componentUnit;

            Doses[0].Quantity.Unit = substanceUnit;
            Doses[0].Total.Unit = substanceUnit;
            Doses[0].Rate.Unit = substanceUnit;

        }
    }
}
