using System;
using System.Collections.Generic;
using Informedica.GenPres.Business.Domain.Patients;
using Informedica.GenPres.Business.Domain.Units;

namespace Informedica.GenPres.Business.Domain.Prescriptions
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

        private string _patientWeightUnit = "kg";
        public virtual string PatientWeightUnit
        {
            get { return _patientWeightUnit; }
            set { _patientWeightUnit = value; SetPatientWeightLengthBaseValue(); }
        }

        private string _patientLengthUnit = "cm";
        public virtual string PatientLengthUnit
        {
            get { return _patientLengthUnit; }
            set { _patientLengthUnit = value; SetPatientWeightLengthBaseValue(); }
        }

        private decimal _patientWeight;
        public virtual decimal PatientWeight
        {
            get { return _patientWeight; }
            set { _patientWeight = value; SetPatientWeightLengthBaseValue(); }
        }

        private decimal _patientLength;
        public virtual decimal PatientLength
        {
            get { return _patientLength; }
            set { _patientLength = value;
            SetPatientWeightLengthBaseValue();
            }
        }

        public virtual void SetPatientWeightLengthBaseValue()
        {
            FirstDose.Quantity.SetWeightLengthBsa(_patientWeight, _patientLength, PatientBsa);
            FirstDose.Total.SetWeightLengthBsa(_patientWeight, _patientLength, PatientBsa);
            FirstDose.Rate.SetWeightLengthBsa(_patientWeight, _patientLength, PatientBsa);
        }


        public virtual Substance FirstSubstance
        {
            get { return Drug.Components[0].Substances[0]; }
        }

        public virtual Component FirstComponent
        {
            get { return Drug.Components[0]; }
        }

        public virtual Dose FirstDose
        {
            get { return Doses[0]; }
        }

        public virtual decimal PatientBsa { get
        {
            return 0.20247m * (decimal)Math.Pow((double)_patientLength, 0.725) * (decimal)Math.Pow((double)UnitConverter.GetUnitValue("kg", _patientWeight), 0.425);
        } }

        public static Prescription NewPrescription()
        {
            var prescription = new Prescription
            {
                Doses = new List<Dose> {Dose.NewDose()},
                Quantity = UnitValue.NewUnitValue(),
                Frequency = UnitValue.NewUnitValue(),
                Total = UnitValue.NewUnitValue(),
                Rate = UnitValue.NewUnitValue(),
                Duration = UnitValue.NewUnitValue(),
            };
            prescription.Frequency.Time = "dag";
            prescription.Total.Time = "dag";
            prescription.Rate.Time = "uur";
            prescription.Duration.Time = "uur";

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
