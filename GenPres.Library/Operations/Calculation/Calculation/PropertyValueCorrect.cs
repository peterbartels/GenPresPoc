using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using GenPres.Business;

namespace GenPres.Operations.Calculation
{
    class PropertyValueCorrect
    {
        /* This method rectifies to the closes possible muiltiplier */
        public static bool CorrectToIncrementSize(Csla.PropertyInfo<UnitValue> propertyName)
        {
            PropertyManager _propertyManager = PropertyManager.Instance;
            Factor propertyFactor = _propertyManager.GetFactor(propertyName);

            decimal[] increments = propertyFactor.IncrementSizes;
            if (increments.Length == 0) return false;

            decimal currentValue = _propertyManager.GetValue(propertyName);

            bool allowIncrementStepping = _propertyManager.GetFactor(propertyName).IncrementStepping;
            
            if (currentValue == 0) return false;

            decimal newValue = 0;
            List<int> foundIndices = new List<int>();

            if (!allowIncrementStepping)
                newValue = MathExt.GetNearestValueByIncrements(currentValue, increments.ToArray(), out foundIndices);
            else
                newValue = MathExt.GetNearestMultiplierByIncrements(currentValue, increments.ToArray(), out foundIndices);

            if ((double)newValue != (double)currentValue)
            {
                _propertyManager.SetValue(propertyName, newValue); 
                return true;
            }

            return false;
        }
    }
}
