using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Operations.Calculation
{
    public static class MathExt
    {
        /*
         * Returns modulo between two decimals
         */ 
        private static decimal _floatMod(decimal a, decimal b)
        {
            return a % b;
        }

        /*
         * Rounds the number to an integer
         */
        public static decimal RoundToInt(decimal a)
        {
            decimal result = Math.Round(a, 0, MidpointRounding.AwayFromZero);
            if (result < 1) result = 1;
            return result;
        }

        /*
         * Fixes rounding problems for decimals
         */
        public static decimal FixPrecision(decimal number)
        {
            return Math.Round(number, 12);
        }

        /*
         * Modulo for two decimals
         */
        public static decimal Mod(decimal a, decimal b)
        {
            a = Math.Round(a, 6);
            b = Math.Round(b, 6);
            return (decimal)(a % b);
        }

        /*
         * Gets the difference between two values
         */
        public static decimal GetValueDiffence(decimal value, decimal minimalValue)
        {
            decimal leftOverValue = _floatMod(value, minimalValue);
            if (value < minimalValue && value != 0)
            {
                value = minimalValue;
                return value;
            }
            if (leftOverValue != 0 && leftOverValue != minimalValue)
            {
                if (leftOverValue >= (minimalValue / 2))
                {
                    value = value + (minimalValue - leftOverValue);
                }
                else
                {
                    value = value - leftOverValue;
                }
                return value;
            }
            return value;
        }

        /*
         * Gets the nearest value from a list of values
         * Returns also the indices from where in the list the values can be found
         */
        public static decimal GetNearestValueByIncrements(decimal val, decimal[] values, out List<int> foundIndices)
        {
            foundIndices = new List<int>();
            decimal returnValue = 0;
            decimal minimalDiff = int.MaxValue;
            for (int s = 0; s < values.Length; s++)
            {
                decimal divValue = values[s];
                decimal diff;
                if (divValue > val)
                {
                    diff = divValue - val;
                }
                else
                {
                    diff = val - divValue;
                }

                if (diff <= minimalDiff)
                {
                    returnValue = divValue;
                    if(diff < minimalDiff) foundIndices.Clear();
                    foundIndices.Add(s);
                    minimalDiff = diff;
                }
            }
            return returnValue;
        }

        /*
         * Gets the nearest multiplier from a list of incrementSizes for a particular value
         */
        public static decimal GetNearestMultiplierByIncrements(decimal val, decimal[] incrementSizes, out List<int> foundIndices)
        {
            foundIndices = new List<int>();
            decimal minimalDiff = int.MaxValue;
            decimal modCheck = 0;
            for (int s = 0; s < incrementSizes.Length; s++)
            {
                decimal value = incrementSizes[s];
                if (val == 0 || value == 0) continue;
                decimal diff = 0;
                decimal mod = _floatMod(val, value);
                if ((value / 2) >= mod)
                {
                    diff = mod;
                }
                else
                {
                    diff = value - mod;
                }
                if (diff <= minimalDiff)
                {
                    if(diff != minimalDiff) modCheck = value;
                    if (diff < minimalDiff) foundIndices.Clear();
                    foundIndices.Add(s);
                    minimalDiff = diff;
                }
            }
            return GetValueDiffence(val, modCheck);
        }


        /*
         * Calculates in a property array for a given index in that array
         */
        internal static decimal CalculateRawValues(int indexToCalculate, decimal[] properties)
        {
            if (indexToCalculate == 0)
                return properties[indexToCalculate] = properties[1] * properties[2];
            if (indexToCalculate == 1)
                return properties[indexToCalculate] = properties[0] / properties[2];
            if (indexToCalculate == 2)
                return properties[indexToCalculate] = properties[0] / properties[1];

            return 0;
        }
    }
}
