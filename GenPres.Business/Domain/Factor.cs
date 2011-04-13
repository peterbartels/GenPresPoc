using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;

namespace GenPres.Business.Domain
{
    internal class Factor
    {
        private PropertyInfo<UnitValue> _unitValue;
        //private GenPres.Operations.Calculation.PropertyManager pm = GenPres.Operations.Calculation.PropertyManager.Instance;

        internal Factor() { }

        internal Factor(PropertyInfo<UnitValue> unitValue)
        {
            _unitValue = unitValue;
        }
        internal PropertyInfo<UnitValue> GetProperty()
        {
            return _unitValue;
        }
        private decimal[] _incrementSizes;
        private decimal _value = 0;

        private bool _incrementStepping;
        internal bool IncrementStepping
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
        internal decimal IncrementStep
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

        internal decimal[] IncrementSizes
        {
            get
            {
                return _incrementSizes;
            }
            set
            {
                _incrementSizes = value;
                if (_unitValue != null)
                {
                    //TODO pm.GetUnitValue(_unitValue, 0).AllowIncrementStep = IncrementStepping;
                    //TODO pm.GetUnitValue(_unitValue, 0).Increments = _incrementSizes;
                    //TODO pm.GetUnitValue(_unitValue, 0).UpdateIncrements(pm.GetPrescription());
                }
            }
        }

        internal decimal Value
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
    }
}
