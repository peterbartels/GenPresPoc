using System;
using System.Linq.Expressions;
using GenPres.Business.Domain.PrescriptionDomain;
using GenPres.Business.Domain.UnitDomain;
using GenPres.Operations.Calculation;

namespace GenPres.Business.Domain.Calculation.Combination
{

    public class MultiplierCombination : ICalculationCombination
    {
        private decimal[] values = new decimal[3];
        private UnitValue[] _unitValues = new UnitValue[3];

        private Expression<Func<UnitValue>>[] _properties;
        private Prescription _root;

        public MultiplierCombination(Prescription root, params Expression<Func<UnitValue>>[] properties)
        {
            _root = root;
            _properties = properties;

            for (int i = 0; i < properties.Length; i++)
            {
                _unitValues[i] = PropertyHelper.GetUnitValue(root, properties[i]);
                values[i] = GetConvertedValue(i);
            }
        }

        internal decimal GetIncrementStep(int index, decimal substanceIncrement)
        {
            Factor propertyFactor = _unitValues[index].Factor;
            decimal increment = propertyFactor.GetIncrementStep(GetValue(index), substanceIncrement);
            return increment;
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
            Expression<Func<UnitValue>> prop = _properties[index];
            string className = PropertyHelper.ClassName(prop);
            string memberName = PropertyHelper.MemberName(prop);

            if (className == "Prescription" && memberName == "Frequency")
                return _unitValues[index].Value;

            if (className == "Prescription" && memberName == "Quantity")
                return _unitValues[index].Value;

            if (className == "Prescription" && memberName == "Total")
                return _unitValues[index].BaseValue / UnitConverter.GetUnitValue(_root.Frequency.Time, 1);

            return 0;
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
    }
}