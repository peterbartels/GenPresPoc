using System.Collections.Generic;
using GenPres.Business.Calculation.Combination;
using GenPres.Business.Domain.Units;

namespace GenPres.Operations.Calculation
{
    
    public class ContinuousCombination : AbstractCombination, ICalculationCombination
    {
        public UnitValue _continuousProperty;
        private List<decimal>[] _calculatedValues;

        #region Constructors
        
        /*
         * Create a new combination with a list of properties
         */
        public ContinuousCombination(UnitValue continuousProperty, UnitValue totalProperty, UnitValue property2, UnitValue property3)
        {
            //_continuousProperty = continuousProperty;
            //_properties.Add(totalProperty);
            //_properties.Add(property2);
            //_properties.Add(property3);
        }
        #endregion

        #region Calculations

        /*
         * Calculates this combination
         */
        public void Calculate()
        {
            int calculateIndex = 0;// GetNonCalculatedFieldIndex();

            if (calculateIndex != -1 /*&& CanBeCalculated()*/)
            {
                decimal[] substanceIncrements = new decimal[] {}; ;//_propertyManager.GetSubstanceIncrementValues();
                decimal continuousValue = 0;// _propertyManager.GetValue(_continuousProperty);
                
                _calculatedValues = new List<decimal>[3] { 
                    new List<decimal>(), new List<decimal>(), new List<decimal>()
                };

                if (continuousValue == 0)
                {
                    //continuousValue = MathExt.CalculateRawValues(GetIndexByProperty(_continuousProperty), GetPropertiesValuesArray());
                }
                
                decimal[] values = new decimal[3];

                foreach (decimal substanceIncrement in substanceIncrements)
                {
                    values = GetCombinationValues(this, values, substanceIncrement);
                    /*
                    values = ContinuousCalculation.GetContinuousIncrementSize(
                        this, _propertyManager.GetValue(_continuousProperty),
                        substanceIncrement,
                        _propertyManager.GetFactor(_continuousProperty)
                    );
                    AddToValues(this, values, substanceIncrement);*/
                }

                int calculatedSubstanceIndex = _findCalculatedIndices();
                /*for (int i = 0; i < _properties.Count; i++)
                {
                    bool didCalculate = _propertyManager.TrySetValue(GetAt(i), _calculatedValues[i][calculatedSubstanceIndex]);
                    //if (didCalculate) CalculateSibblings(i);
                }*/
            }
        }

        public void Finish()
        {
            throw new System.NotImplementedException();
        }

        public void ConvertCombinationsValuesToArray()
        {
            throw new System.NotImplementedException();
        }

        public void CorrectPropertyIncrements()
        {
            throw new System.NotImplementedException();
        }


        private int _findCalculatedIndices()
        {
            /*List<int> foundIndices = new List<int>();
            for (int i = 0; i < _calculatedValues.Length; i++)
            {
                if (_calculatedValues[i].Contains(0)) continue;
                if (_propertyManager.GetValue(GetAt(i)) == 0) continue;

                List<int> indices;
                decimal nearestVal = MathExt.GetNearestValueByIncrements(
                    _propertyManager.GetValue(GetAt(i)),
                    _calculatedValues[i].ToArray(),
                    out indices
                );
                if (foundIndices.Count == 0) foundIndices = indices;
                if (indices.Count < foundIndices.Count) foundIndices = indices;
            }
            return foundIndices[0];*/
            return 0;
        }

        private void AddToValues(AbstractCombination combination, decimal[] values, decimal increment)
        {
            var valueArray = new decimal[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                //_calculatedValues[i].Add(GetIncrementValue(i, values[i], increment));
            }
        }


        private decimal[] GetCombinationValues(AbstractCombination combination, decimal[] values, decimal increment)
        {
            for (int i = 0; i < values.Length; i++)
            {
                //values[i] = GetIncrementStep(i, increment);
            }
            return values;
        }

        /* 
         * Gets the continuous property (if available)
         */
        internal UnitValue GetContinuousProperty()
        {
            return _continuousProperty;
        }

        #endregion

    }
}
