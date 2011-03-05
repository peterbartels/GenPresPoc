using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using GenPres.Business;

namespace GenPres.Operations.Calculation
{
    /*
     * This class contains operations and properties to form a combination
     */
    public class MultiplierCombination : AbstractCombination, ICalculationCombination
    {
        public PropertyInfo<UnitValue> _dependencyTarget;
        public PropertyInfo<UnitValue> _dependencySource;

        #region Constructors
        
        /*
         * Create a new combination with a list of properties
         */
        public MultiplierCombination(PropertyInfo<UnitValue> totalProperty, PropertyInfo<UnitValue> property2, PropertyInfo<UnitValue> property3)
        {
            _properties.Add(totalProperty);
            _properties.Add(property2);
            _properties.Add(property3);
        }
        #endregion

        #region Calculations

        /*
         * Calculates this combination
         */
        public void Calculate()
        {
            if (CanBeCalculated())
            {
                int calculateIndex = GetNonCalculatedFieldIndex();
                decimal[] originalSizes = new decimal[] { };
                if (_dependencyTarget != null && !_propertyManager.IsPrescriptionSolution())
                {
                    originalSizes = _propertyManager.GetFactor(Dose.QuantityProperty).IncrementSizes;
                }
                bool dependentContinuous = CheckDependency(ref calculateIndex);

                PropertyCombinationCalculate pcc = new PropertyCombinationCalculate();
                bool didCalculate = pcc.Calculate(this, calculateIndex);
                if (dependentContinuous)
                {
                    CalculateSibblings(GetIndexByProperty(_dependencyTarget));
                }
                
                if (didCalculate) CalculateSibblings(calculateIndex);
                if (_dependencyTarget != null && !_propertyManager.IsPrescriptionSolution())
                {
                    _propertyManager.GetFactor(Dose.QuantityProperty).IncrementSizes = originalSizes;
                    _propertyManager.GetFactor(Dose.TotalProperty).IncrementSizes = originalSizes;
                    _propertyManager.GetFactor(Dose.RateProperty).IncrementSizes = originalSizes;
                }
            }
        }

        private bool CheckDependency(ref int calculateIndex)
        {
            bool dependentContinuous = false;
            if ( _dependencyTarget != null)
            {
                if (_propertyManager.GetValue(_dependencyTarget) == 0)
                {
                    dependentContinuous = true; 
                    decimal[] values = GetPropertiesValuesArray();
                    decimal continuousValue = MathExt.CalculateRawValues(GetIndexByProperty(_dependencyTarget), values);
                    _propertyManager.SetBaseValue(_dependencyTarget, continuousValue);

                    ContinuousCombination c = (ContinuousCombination)FindCombinationByProperty(_dependencyTarget);
                    c.Calculate();

                    if (_propertyManager.GetValue(GetDependentProperty()) != 0)
                    {
                        calculateIndex = GetIndexByProperty(GetDependentProperty());
                    }
                }

                Factor contFactor = _propertyManager.GetFactor(_dependencyTarget);
                Factor depFactor = _propertyManager.GetFactor(GetDependentProperty());
                
                decimal[] iSizes = new decimal[] { _propertyManager.GetBaseValue(_dependencyTarget) * _propertyManager.GetComponentIncrementValue()};
                
                _propertyManager.GetFactor(Dose.QuantityProperty).IncrementSizes = iSizes;
                _propertyManager.GetFactor(Dose.TotalProperty).IncrementSizes = iSizes;
                _propertyManager.GetFactor(Dose.RateProperty).IncrementSizes = iSizes;


                contFactor.IncrementSizes = new decimal[] { _propertyManager.GetBaseValue(_dependencyTarget) };
                contFactor.Value = 0;
                return dependentContinuous;
            }
            return false;
        }


        #endregion

        #region Continuous Operations

        /* 
         * Gets the continuous property (if available)
         */
        internal PropertyInfo<UnitValue> GetDependentProperty()
        {
            return (from prop in _properties where (
                        prop == Dose.QuantityProperty || 
                        prop == Dose.TotalProperty ||
                        prop == Dose.RateProperty
                        ) 
                    select prop).FirstOrDefault<PropertyInfo<UnitValue>>();
        }


        /*
         * Sets a given source property to be depended on a target property
         */
        public void SetDependency(PropertyInfo<UnitValue> dependencySource, PropertyInfo<UnitValue> dependencyTarget)
        {
            dependendContinuousProperty = dependencySource;
            _dependencyTarget = dependencyTarget;
        }

        #endregion
    }
}