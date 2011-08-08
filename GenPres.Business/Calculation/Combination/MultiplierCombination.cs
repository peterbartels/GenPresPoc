using System;
using System.IO;
using System.Linq.Expressions;
using GenPres.Business.Calculation.Calculation;
using GenPres.Business.Calculation.Math;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Business.Domain.Units;

namespace GenPres.Business.Calculation.Combination
{

    public class MultiplierCombination : ICalculationCombination
    {
        private decimal[] values = new decimal[3];
        private UnitValue[] _unitValues = new UnitValue[3];

        private Expression<Func<UnitValue>>[] _properties;
        private IPrescription _root;

        public MultiplierCombination(IPrescription root, params Expression<Func<UnitValue>>[] properties)
        {
            _root = root;
            _properties = properties;

            ConvertCombinationsValuesToArray();
        }

        internal decimal GetIncrementStep(int index, decimal substanceIncrement)
        {
            Factor propertyFactor = _unitValues[index].Factor;
            decimal increment = propertyFactor.GetIncrementStep(GetValue(index), substanceIncrement);
            return System.Math.Round(increment, 8, MidpointRounding.AwayFromZero);
        }

        internal decimal GetIncrementValue(int index, decimal incrementStep, decimal substanceIncrement)
        {
            Factor propertyFactor = _unitValues[index].Factor;
            decimal increment = propertyFactor.GetIncrementValue(incrementStep, substanceIncrement);
            return increment;
        }

        public decimal GetValue(int index)
        {
            return values[index];
        }

        public void SetValue(int index, decimal value)
        {
            values[index] = value;
        }

        public decimal GetConvertedValue(int index)
        {
            var prop = _properties[index];
            var className = PropertyHelper.ClassName(prop);
            var memberName = PropertyHelper.MemberName(prop);

            if (className == "IPrescription" && memberName == "Frequency")
                return _unitValues[index].BaseValue / UnitConverter.GetUnitValue(_root.Frequency.Time, 1);

            if (className == "IPrescription" && memberName == "Quantity")
                return _unitValues[index].BaseValue;

            if (className == "IPrescription" && memberName == "Total")
                return _unitValues[index].BaseValue / UnitConverter.GetUnitValue(_root.Frequency.Time, 1);

            return 0;
        }

        public decimal SetBaseValue(int index)
        {
            Expression<Func<UnitValue>> prop = _properties[index];
            string className = PropertyHelper.ClassName(prop);
            string memberName = PropertyHelper.MemberName(prop);

            if (className == "IPrescription" && memberName == "Frequency")
                _unitValues[index].BaseValue = values[index] * UnitConverter.GetUnitValue(_root.Frequency.Time, 1); ;

            if (className == "IPrescription" && memberName == "Quantity")
                _unitValues[index].BaseValue = values[index];

            if (className == "IPrescription" && memberName == "Total")
                _unitValues[index].BaseValue = values[index] * UnitConverter.GetUnitValue(_root.Frequency.Time, 1);

            return 0;
        }

        public UnitValue GetUnitValue(int index)
        {
            return _unitValues[index];
        }

        public void Calculate()
        {
            var pc = new PropertyCombinationCalculate();
            pc.Calculate(this, 2);
        }

        public bool CanBeCalculated()
        {
            return true;
        }

        public void Finish()
        {
            for (int i = 0; i < values.Length; i++)
            {
                SetBaseValue(i);
            }
        }

        public bool Validate()
        {
            File.AppendAllText(@"c:\temp\test.txt", "2: Value0=" + MathExt.FixPrecision(values[0]) + " Value1=" + MathExt.FixPrecision(values[1]) + " Value2=" + MathExt.FixPrecision(values[2]) + "\r\n");
            return MathExt.FixPrecision(values[0]) == MathExt.FixPrecision(values[1])*MathExt.FixPrecision(values[2]);
        }

        public void ConvertCombinationsValuesToArray()
        {
            for (int i = 0; i < _properties.Length; i++)
            {
                _unitValues[i] = PropertyHelper.GetUnitValue(_root, _properties[i]);
                values[i] = GetConvertedValue(i);
            }
        }
    }
}