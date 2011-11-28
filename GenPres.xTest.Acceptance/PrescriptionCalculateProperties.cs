using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.xTest.Acceptance
{
    public class PrescriptionCalculateProperties
    {
        protected Prescription _prescription = Prescription.NewPrescription();

        public string Frequency
        {
            get { return _prescription.Frequency.Value.ToString("0.####"); }
            set { _prescription.Frequency.Value = value == "" ? 0 : decimal.Parse(value); }
        }

        public string DoseQuantity
        {
            get { return _prescription.FirstDose.Quantity.Value.ToString("0.####"); }
            set { _prescription.FirstDose.Quantity.Value = value == "" ? 0 : decimal.Parse(value); }
        }
        public string DoseTotal
        {
            get { return _prescription.FirstDose.Total.Value.ToString("0.####"); }
            set { _prescription.FirstDose.Total.Value = value == "" ? 0 : decimal.Parse(value); }
        }
        public string AdminQuantity
        {
            get
            {
                return _prescription.Quantity.Value.ToString("0.####");
            }
            set { _prescription.Quantity.Value = value == "" ? 0 : decimal.Parse(value); }
        }
        public string AdminTotal
        {
            get { return _prescription.Total.Value.ToString("0.####"); }
            set { _prescription.Total.Value = value == "" ? 0 : decimal.Parse(value); }
        }

        public string DrugQuantity
        {
            get
            {
                return _prescription.Drug.Quantity.Value.ToString("0.####");
            }
            set { _prescription.Drug.Quantity.Value = value == "" ? 0 : decimal.Parse(value); }
        }

        public string SubstanceQuantity
        {
            get
            {
                return _prescription.FirstSubstance.Quantity.Value.ToString("0.####");
            }
            set { _prescription.FirstSubstance.Quantity.Value = value == "" ? 0 : decimal.Parse(value); }
        }

        public string SubstanceDrugConcentration
        {
            get
            {
                return _prescription.FirstSubstance.DrugConcentration.Value.ToString("0.####");
            }
            set { _prescription.FirstSubstance.DrugConcentration.Value = value == "" ? 0 : decimal.Parse(value); }
        }

        public string DoseRate
        {
            get
            {
                return _prescription.FirstDose.Rate.Value.ToString("0.####");
            }
            set { _prescription.FirstDose.Rate.Value = value == "" ? 0 : decimal.Parse(value); }
        }

        public string AdminRate
        {
            get
            {
                return _prescription.Rate.Value.ToString("0.####");
            }
            set { _prescription.Rate.Value = value == "" ? 0 : decimal.Parse(value); }
        }

        public string Duration
        {
            get
            {
                return _prescription.Duration.Value.ToString("0.####");
            }
            set { _prescription.Duration.Value = value == "" ? 0 : decimal.Parse(value); }
        }


        public string FrequencyUnit
        {
            get { return _prescription.Frequency.Time; }
            set
            {
                _prescription.Frequency.Time = value;
            }
        }

        public string DoseQuantityUnit
        {
            get { return _prescription.FirstDose.Quantity.Unit; }
            set
            {
                _prescription.FirstDose.Quantity.Unit = value;
            }
        }
        public string DoseTotalUnit
        {
            get { return _prescription.FirstDose.Total.Unit; }
            set
            {
                _prescription.FirstDose.Total.Unit = value;
            }
        }
        public string DoseTotalTime
        {
            get { return _prescription.FirstDose.Total.Time; }
            set
            {
                _prescription.FirstDose.Total.Time = value;
            }
        }

        public string AdminQuantityUnit
        {
            get { return _prescription.Quantity.Unit; }
            set
            {
                _prescription.Quantity.Unit = value;
            }
        }
        public string AdminTotalUnit
        {
            get { return _prescription.Total.Unit; }
            set
            {
                _prescription.Total.Unit = value;
            }
        }
        public string AdminTotalTime
        {
            get { return _prescription.Total.Time; }
            set
            {
                _prescription.Total.Time = value;
            }
        }

        public string DrugQuantityUnit
        {
            get { return _prescription.Drug.Quantity.Unit; }
            set
            {
                _prescription.Drug.Quantity.Unit = value;
            }
        }

        public string SubstanceDrugConcentrationUnit
        {
            get { return _prescription.FirstSubstance.DrugConcentration.Unit; }
            set
            {
                _prescription.FirstSubstance.DrugConcentration.Unit = value;
            }
        }

        public string SubstanceDrugConcentrationTotal
        {
            get { return _prescription.FirstSubstance.DrugConcentration.Total; }
            set
            {
                _prescription.FirstSubstance.DrugConcentration.Total = value;
            }
        }

        public string DoseRateUnit
        {
            get { return _prescription.FirstDose.Rate.Unit; }
            set
            {
                _prescription.FirstDose.Rate.Unit = value;
            }
        }

        public string DoseRateTime
        {
            get { return _prescription.FirstDose.Rate.Time; }
            set
            {
                _prescription.FirstDose.Rate.Time = value;
            }
        }

        public string AdminRateUnit
        {
            get { return _prescription.Rate.Unit; }
            set
            {
                _prescription.Rate.Unit = value;
            }
        }

        public string AdminRateTime
        {
            get { return _prescription.Rate.Time; }
            set
            {
                _prescription.Rate.Time = value;
            }
        }

        public string DurationTime
        {
            get { return _prescription.Duration.Time; }
            set
            {
                _prescription.Duration.Time = value;
            }
        }



    }
}
