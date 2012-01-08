using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Business.Domain.Units;

namespace GenPres.xTest.Acceptance
{
    public class PrescriptionCalculateProperties
    {
        protected Prescription _prescription = Prescription.NewPrescription();

        public string Generic
        {
            get { return _prescription.Drug.Generic; }
            set { _prescription.Drug.Generic = value; }
        }

        public string Frequency
        {
            get { return _prescription.Frequency.Value.ToString("0.####"); }
            set
            {
                SetValueWithTime(value, _prescription.Frequency);
            }
        }

        public string DoseQuantity
        {
            get { return _prescription.FirstDose.Quantity.Value.ToString("0.####"); }
            set
            {
                SetValueWithUnitAndAdjust(value, _prescription.FirstDose.Quantity);
            }
        }

        public string DoseTotal
        {
            get { return _prescription.FirstDose.Total.Value.ToString("0.####"); }
            set
            {
                SetValueWithUnitTimeAndAdjust(value, _prescription.FirstDose.Total);
            }
        }
        public string AdminQuantity
        {
            get
            {
                return _prescription.Quantity.Value.ToString("0.####");
            }
            set
            {
                SetValueWithUnitAndAdjust(value, _prescription.Quantity);
            }
        }
        public string AdminTotal
        {
            get { return _prescription.Total.Value.ToString("0.####"); }
            set
            {
                SetValueWithUnitTimeAndAdjust(value, _prescription.Total);
            }
        }

        public string DrugQuantity
        {
            get
            {
                return _prescription.Drug.Quantity.Value.ToString("0.####");
            }
            set
            {
                SetValueWithUnitAndAdjust(value, _prescription.Drug.Quantity);
            }
        }

        public string SubstanceQuantity
        {
            get
            {
                return _prescription.FirstSubstance.Quantity.Value.ToString("0.####");
            }
            set
            {
                SetValueWithUnitAndAdjust(value, _prescription.FirstSubstance.Quantity);
            }
        }

        public string SubstanceDrugConcentration
        {
            get
            {
                return _prescription.FirstSubstance.DrugConcentration.Value.ToString("0.####");
            }
            set
            {
                SetValueWithUnitTotal(value, _prescription.FirstSubstance.DrugConcentration);
            }
        }

        public string DoseRate
        {
            get
            {
                return _prescription.FirstDose.Rate.Value.ToString("0.####");
            }
            set
            {
                SetValueWithUnitTimeAndAdjust(value, _prescription.FirstDose.Rate);
            }
        }

        public string AdminRate
        {
            get
            {
                return _prescription.Rate.Value.ToString("0.####");
            }
            set { 
                SetValueWithUnitTimeAndAdjust(value, _prescription.Rate);
            }
        }

        public string Duration
        {
            get
            {
                return _prescription.Duration.Value.ToString("0.####");
            }
            set
            {
                SetValueWithTime(value, _prescription.Duration);
            }
        }


        public string Time
        {
            get
            {
                return Duration;
            }
            set { Duration = value; }
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
                SetUnitWithTimeAndAdjust(value, _prescription.FirstDose.Total);
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
                SetUnitWithTime(value, _prescription.Total); 
            }
        }
        public string DrugQuantityUnit
        {
            get { return _prescription.Drug.Quantity.Unit; }
            set
            {
                SetUnitWithAdjust(value, _prescription.Drug.Quantity);
            }
        }

        public string SubstanceQuantityUnit
        {
            get { return _prescription.FirstSubstance.Quantity.Unit; }
            set
            {
                SetUnitWithAdjust(value, _prescription.FirstSubstance.Quantity);
            }
        }

        public string SubstanceDrugConcentrationUnit
        {
            get { return _prescription.FirstSubstance.DrugConcentration.Unit; }
            set
            {
                SetUnitWithTotal(value, _prescription.FirstSubstance.DrugConcentration);
            }
        }

        public string DoseRateUnit
        {
            get { return _prescription.FirstDose.Rate.Unit; }
            set
            {
                SetUnitWithTimeAndAdjust(value, _prescription.FirstDose.Rate);
            }
        }


        public string AdminRateUnit
        {
            get { return _prescription.Rate.Unit; }
            set
            {
                SetUnitWithTimeAndAdjust(value, _prescription.Rate);
            }
        }

        public string DurationUnit
        {
            get { return _prescription.Duration.Time; }
            set
            {
                _prescription.Duration.Time = value;
            }
        }

        private static void SetUnitWithAdjust(string input, UnitValue uv)
        {
            var inputSplit = input.Trim().Split('/');
            
            uv.Unit = inputSplit[0].Trim();
            
            if(inputSplit.Length == 2)
                uv.Adjust = inputSplit[1].Trim();   
        }

        private static void SetUnitWithTime(string input, UnitValue uv)
        {
            var inputSplit = input.Trim().Split('/');

            uv.Unit = inputSplit[0].Trim();

            if (inputSplit.Length == 2)
                uv.Time = inputSplit[1].Trim();
        }

        private static void SetTime(string input, UnitValue uv)
        {
            var inputSplit = input.Trim().Split('/');
            uv.Time = inputSplit[0].Trim();
        }

        private static void SetUnitWithTotal(string input, UnitValue uv)
        {
            var inputSplit = input.Trim().Split('/');
            uv.Unit = inputSplit[0].Trim();
            uv.Total = inputSplit[1].Trim();
        }

        private static void SetUnitWithTimeAndAdjust(string input, UnitValue uv)
        {
            var inputSplit = input.Trim().Split('/');

            uv.Unit = inputSplit[0].Trim();

            if (inputSplit.Length == 2)
                uv.Time = inputSplit[1].Trim();

            if (inputSplit.Length == 3)
            {
                uv.Time = inputSplit[1].Trim();
                uv.Adjust = inputSplit[2].Trim();
            }
        }

        private static void SetValueWithTime(string input, UnitValue uv)
        {
            uv.Value = ExtractValue(input);
            string units = ExtractUnits(input);
            if (units != "") SetTime(units, uv);
        }

        private static void SetValueWithUnitAndAdjust(string input, UnitValue uv)
        {
            uv.Value = ExtractValue(input);
            string units = ExtractUnits(input);
            if (units != "") SetUnitWithAdjust(units, uv);
        }


        private static void SetValueWithUnitTotal(string input, UnitValue uv)
        {
            uv.Value = ExtractValue(input);
            string units = ExtractUnits(input);
            if (units != "") SetUnitWithTotal(units, uv);
        }

        private static void SetValueWithUnitTimeAndAdjust(string input, UnitValue uv)
        {
            uv.Value = ExtractValue(input);
            string units = ExtractUnits(input);
            if (units != "") SetUnitWithTimeAndAdjust(units, uv);
        }

        private static string ExtractUnits(string input)
        {
            if(input.Trim() == "") return "";
            return Regex.Replace(Regex.Match(input.Trim(), @"^[\d|\.]*(\D*)").Groups[1].Value.Trim(), @"^\/", "").Trim();
        }

        private static decimal ExtractValue(string input)
        {
            if (input == "") return 0;
            return decimal.Parse(Regex.Match(input, @"(^[\d|\.]*)").Groups[1].Value);
        }
    }
}
