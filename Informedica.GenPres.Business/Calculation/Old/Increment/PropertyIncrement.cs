using System.Collections.Generic;
using System.Linq;
using GenPres.Business.Calculation.Old.Math;
using GenPres.Business.Domain.Units;

namespace GenPres.Business.Calculation.Old.Increment
{
    public class PropertyIncrement
    {
        public static bool CorrectPropertyIncrement(UnitValue incrementProperty, ref decimal value)
        {
            var increments = incrementProperty.Factor.IncrementSizes;
            if (increments.Length == 0) return false;

            var currentValue = value;

            var allowIncrementStepping = incrementProperty.Factor.IncrementStepping;

            if (currentValue == 0) return false;

            decimal newValue = 0;

            List<int> foundIndices;

            if (!allowIncrementStepping)
                newValue = MathExt.GetNearestValueByIncrements(currentValue, increments.ToArray(), out foundIndices);
            else
                newValue = MathExt.GetNearestMultiplierByIncrements(currentValue, increments.ToArray(), out foundIndices);

            if (newValue != currentValue)
            {
                value = newValue;
                return true;
            }

            return false;

        }
    }
}
