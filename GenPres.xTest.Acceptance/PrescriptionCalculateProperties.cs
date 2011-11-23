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
            set { _prescription.Frequency.Value = decimal.Parse(value); }
        }

        public string DoseQuantity
        {
            get { return _prescription.FirstDose.Quantity.Value.ToString("0.####"); }
            set { _prescription.FirstDose.Quantity.Value = decimal.Parse(value); }
        }
        public string DoseTotal
        {
            get { return _prescription.FirstDose.Total.Value.ToString("0.####"); }
            set { _prescription.FirstDose.Total.Value = decimal.Parse(value); }
        }
        public string AdminQuantity
        {
            get
            {
                return _prescription.Quantity.Value.ToString("0.####");
            }
            set { _prescription.Quantity.Value = decimal.Parse(value); }
        }
        public string AdminTotal
        {
            get { return _prescription.Total.Value.ToString("0.####"); }
            set { _prescription.Total.Value = decimal.Parse(value); }
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
        public string DoseTotalTimeUnit
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
        public string AdminTotalTimeUnit
        {
            get { return _prescription.Total.Time; }
            set
            {
                _prescription.Total.Time = value;
            }
        }

    }
}
