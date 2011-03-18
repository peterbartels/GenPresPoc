﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business;

namespace GenPres.Operations.Calculation
{
    class PropertyCombinationCalculate
    {
        private PropertyManager _propertyManager = PropertyManager.Instance;
        private MultiplierCombination _combination;
        private List<decimal>[] _calculatedValues = new List<decimal>[3] { 
            new List<decimal>(), new List<decimal>(), new List<decimal>()
        };

        /*
         * Calculate values based on discrete mathematics
         * First get the incrementStep from the property value then calculate the non calculated field
         * This method also corrects a property if the current values cannot be set.
         */
        public bool Calculate(MultiplierCombination combination, int index)
        {
            _combination = combination;

            /*
             * If not enough properties are available for calculation, 
             * it is not possible to calculate it
             */
            if (!_combination.CanBeCalculated()) return false;

            //In case multiple substance increments are available, retrieve all fd 
            decimal[] substanceIncrements = _propertyManager.GetSubstanceIncrementValues();

            decimal[] values = new decimal[3];

            //Get the property to be corrected
            Csla.PropertyInfo<UnitValue> propertyToRectify = _combination.GetPropertyToRectify();
            int correctIndex = _combination.GetIndexByProperty(propertyToRectify);

            /*
             * Loop through all possible substanceIncrements to calculate the index (=not calculated property in combination)
             * The calculated result is rounded to an integer for the closest possible value
             * After that the value is returned to an incrementValue and added to the list of possible values
             */
            foreach (decimal increment in substanceIncrements)
            {
                //Get IncrementSteps
                values = GetCombinationValues(combination, values, increment);
                
                values[index] = MathExt.RoundToInt(_calculate(index, values));   

                values[correctIndex] = 0;
                _calculate(correctIndex, values);

                AddToValues(combination, values, increment);
            }

            //Determine the closes possible value determined by the user
            int calculatedSubstanceIndex = _findCalculatedIndices();

            //Set the calculated and corrected value, and if a calculation is made, calculate any sibblings
            bool didCorrectValue = _propertyManager.TrySetValue(propertyToRectify, _calculatedValues[correctIndex][calculatedSubstanceIndex]);
            bool didCalculateValue = _propertyManager.TrySetValue(_combination.GetAt(index), _calculatedValues[index][calculatedSubstanceIndex]);
            //if (didCorrectValue) _combination.CalculateSibblings(correctIndex);
            return didCalculateValue;
        }

        private int _findCalculatedIndices()
        {
            List<int> foundIndices = new List<int>();
            for (int i = 0; i < _calculatedValues.Length; i++)
            {
                if (_calculatedValues[i].Contains(0)) continue;
                if (_propertyManager.GetValue(_combination.GetAt(i)) == 0) continue;
                
                List<int> indices;
                
                decimal nearestVal = MathExt.GetNearestValueByIncrements(
                    _propertyManager.GetValue(_combination.GetAt(i)),
                    _calculatedValues[i].ToArray(),
                    out indices
                );
                if (foundIndices.Count == 0) foundIndices = indices;
                if (indices.Count < foundIndices.Count) foundIndices = indices;
            }
            return foundIndices[0];
        }

        private decimal[] GetCombinationValues(MultiplierCombination combination, decimal[] values, decimal increment)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = _combination.GetIncrementStep(i, increment);
            }
            return values;
        }
        /*
         * Method to calculate a value combination based on an index
         */
        private decimal _calculate(int index, decimal[] valCombination)
        {
            if (index == 0) return valCombination[0] = valCombination[1] * valCombination[2];
            if (index == 1) return valCombination[1] = valCombination[0] / valCombination[2];
            if (index == 2) return valCombination[2] = valCombination[0] / valCombination[1];
            return 0;
        }

        private void AddToValues(MultiplierCombination combination, decimal[] values, decimal increment)
        {
            decimal[] valueArray = new decimal[values.Length];
            
            for (int i = 0; i < values.Length; i++)
            {
                _calculatedValues[i].Add(_combination.GetIncrementValue(i, values[i], increment));
            }
        }
    }
}