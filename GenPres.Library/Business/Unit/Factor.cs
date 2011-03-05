using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using GenPres.Operations.Calculation;

namespace GenPres.Business
{
    public class Factor
    {
        private PropertyInfo<UnitValue> _unitValue;
        private GenPres.Operations.Calculation.PropertyManager pm = GenPres.Operations.Calculation.PropertyManager.Instance;

        public Factor() { }

        public Factor(PropertyInfo<UnitValue> unitValue)
        {
            _unitValue = unitValue;
        }
        public PropertyInfo<UnitValue> GetProperty()
        {
            return _unitValue;
        }
        private decimal[] _incrementSizes;
        private decimal _value = 0;
        
        private bool _incrementStepping;
        public bool IncrementStepping
        {
            get
            {
                return _incrementStepping;
            }
            set
            {
                _incrementStepping = value;
            }
        }

        private decimal _incrementStep;
        public decimal IncrementStep
        {
            get
            {
                return _incrementStep;
            }
            set
            {
                _incrementStep = value;
            }
        }

        public decimal[] IncrementSizes {
            get{
                return _incrementSizes;
            }
            set{
                _incrementSizes = value;
                if (_unitValue != null)
                {
                    pm.GetUnitValue(_unitValue, 0).AllowIncrementStep = IncrementStepping;
                    pm.GetUnitValue(_unitValue, 0).Increments = _incrementSizes;
                    pm.GetUnitValue(_unitValue, 0).UpdateIncrements(pm.GetPrescription());
                }
            }
        }

        public decimal Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        
        public decimal GetIncrementValue(decimal incrementStep, decimal substanceIncrement)
        {
            if (Value > 0) return Value;
            if (IncrementSizes == null) return 0;
            decimal? increment = (from i in IncrementSizes where i == substanceIncrement select i).SingleOrDefault<decimal>();
            if (increment == 0) (from i in IncrementSizes where i == substanceIncrement select i).SingleOrDefault<decimal>();

            if (increment == 0 && IncrementSizes.Length > 0) increment = IncrementSizes.FirstOrDefault<decimal>();
            if (increment != 0)
                return MathExt.FixPrecision(incrementStep * increment.Value);

            return incrementStep * 0.00000000000000001m;
        }
        public decimal GetIncrementStep(decimal substanceIncrement)
        {
            return GetIncrementStep(pm.GetValue(_unitValue), substanceIncrement);
        }

        public decimal GetIncrementStep(decimal value, decimal substanceIncrement)
        {
            decimal? incrementSize = GetIncrementSize(substanceIncrement);
            if (incrementSize != 0)
            {
                return MathExt.FixPrecision(value / incrementSize.Value);
            }

            return value / (0.00000000000000001m);
        }
        public bool HasNoIncrements()
        {
            return (IncrementSizes.Length == 0);
        }
        public decimal GetIncrementSize(decimal substanceIncrement)
        {
            if (Value > 0) return Value;
            decimal incrementSize = (from i in IncrementSizes where i == substanceIncrement select i).SingleOrDefault<decimal>();
            if (incrementSize == 0 && IncrementSizes.Length > 0)
            {
                incrementSize = IncrementSizes.FirstOrDefault<decimal>();
            }
            return incrementSize;
        }
    }
}
