using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business;
using Csla;

namespace GenPres.Operations.Calculation
{
    public class AbstractCombination: ICalculationCombination
    {
        protected List<PropertyInfo<UnitValue>> _properties = new List<PropertyInfo<UnitValue>>();
        protected PropertyManager _propertyManager = PropertyManager.Instance;
        protected PropertyInfo<UnitValue> _lastPropertyCalculated = null;
        protected PropertyInfo<UnitValue> dependendContinuousProperty;


        public AbstractCombination()
        { }

        public AbstractCombination(PropertyInfo<UnitValue> totalProperty, PropertyInfo<UnitValue> property2, PropertyInfo<UnitValue> property3)
        {
            _properties.Add(totalProperty);
            _properties.Add(property2);
            _properties.Add(property3);
        }

        #region States

        /*
         * Gets the index of a field by a given state;
         */
        internal int? GetFieldIndexByState(StateManager.State state)
        {
            if (_propertyManager.GetState(_properties[0]) == state) return 0;
            if (_propertyManager.GetState(_properties[1]) == state) return 1;
            if (_propertyManager.GetState(_properties[2]) == state) return 2;
            return null;
        }

        /*
         * Gets the field from the list that is not calculated
         */
        internal int GetNonCalculatedFieldIndex()
        {
            int value = -1;
            PropertyInfo<UnitValue> prop = (from i in _properties where _propertyManager.GetState(i) == StateManager.State.NotSet select i).FirstOrDefault<PropertyInfo<UnitValue>>();

            if (prop == null) return GetIndexByProperty(GetPropertyToRectify());
            value = GetIndexByProperty(prop);
            return value;
        }
        #endregion

        #region Property Operations

        /*
         * Set the last property calculated in case Sibblings are calculated as well
         */
        public void SetLastPropertyCalculated(PropertyInfo<UnitValue> lastPropertyCalculated)
        {
            _lastPropertyCalculated = lastPropertyCalculated;
        }

        public bool HasDependent()
        {
            return (dependendContinuousProperty != null);
        }

        /*
         * Gets the property at a given index
         */
        public PropertyInfo<UnitValue> GetAt(int index)
        {
            switch (index)
            {
                case 0: return _properties[0];
                case 1: return _properties[1];
                case 2: return _properties[2];
            }
            throw new IndexOutOfRangeException("Cannot retrieve index " + index + " from combination.");
        }

        public bool CanBeUsedForIncrementDetermination(PropertyInfo<UnitValue> property)
        {
            if (_propertyManager.GetState(property) == StateManager.State.User) 
                return true;

            return DependencyCombinationCanBeUsedForDetermination(property, false);
        }

        public bool DependencyCombinationCanBeUsedForDetermination(PropertyInfo<UnitValue> property, bool findContinuousCombination)
        {
            foreach (AbstractCombination combi in _propertyManager.incrementCombinationList)
            {
                if (combi.Contains(property) && combi != this)
                {
                    return (combi.GetSetCount() > 1);
                }
            }
            return false;
        }

        /*
         * Gets the index of a given property
         */
        internal int GetIndexByProperty(PropertyInfo<UnitValue> property)
        {
            if (_properties[0] == property) return 0;
            if (_properties[1] == property) return 1;
            if (_properties[2] == property) return 2;
            return -1;
        }

        /*
         * Gets the first property that needs to be corrected
         */
        internal PropertyInfo<UnitValue> GetPropertyToRectify()
        {
            PropertyInfo<UnitValue> prop = (
                from i in _propertyManager.GetSequenceList()
                where
                    (_propertyManager.GetState(i) == StateManager.State.User || _propertyManager.GetState(i) == StateManager.State.Calculated)
                    && _properties.Contains(i)
                orderby _propertyManager.GetState(i).ToString() descending
                select i
            ).FirstOrDefault<PropertyInfo<UnitValue>>();
            return prop;
        }

        /*
         * Get all the properties in the list as an array
         */
        internal decimal[] GetPropertiesValuesArray()
        {
            return (from i in _properties select _propertyManager.GetValue(i)).ToArray<decimal>();
        }

        internal void CorrectPropertyValues()
        {
            if (GetNotSetCount() > 0)
            {
                for (int i = 0; i < _properties.Count; i++)
                {
                    if (_propertyManager.GetState(_properties[i]) == StateManager.State.User)
                        PropertyValueCorrect.CorrectToIncrementSize(_properties[i]);
                }
            }
        }

        /*
         * Returns wether this combination contains the given property
         */
        public bool Contains(PropertyInfo<UnitValue> propertyToContain)
        {
            //TODO use all properties
            return (_properties[0] == propertyToContain || _properties[1] == propertyToContain || _properties[2] == propertyToContain);
        }


        public int GetNotSetCount()
        {
            int notSetCount = 0;
            if (_notSet(_properties[0])) notSetCount++;
            if (_notSet(_properties[1])) notSetCount++;
            if (_notSet(_properties[2])) notSetCount++;
            return notSetCount;
        }


        /*
         * Calculates all sibblings from a certain property at a given index
         */
        public void CalculateSibblings(int index)
        {
            for (int i = _propertyManager.GetSequenceList().Count - 1; i>=0; i--)
            {
                PropertyInfo<UnitValue> prop = _propertyManager.GetSequenceList()[i];
                CalculateSibbling(prop);
            }
        }

        public void CalculateSibbling(PropertyInfo<UnitValue> property)
        {
            foreach (ICalculationCombination pc in _propertyManager.propertyCombinationList)
            {
                if (pc.Contains(property) && pc != this)
                {
                    pc.SetLastPropertyCalculated(property);
                    pc.Calculate();
                    pc.SetLastPropertyCalculated(null);
                }
            }
        }

        public void Calculate(){
            if(CanBeCalculated()){
                int nonCalculatedFieldIndex = GetNonCalculatedFieldIndex();
                decimal[] properties = GetPropertiesValuesArray();
                decimal result = MathExt.CalculateRawValues(nonCalculatedFieldIndex, properties);
                _propertyManager.TrySetValue(GetAt(nonCalculatedFieldIndex), result);
            }
        }

        /*
         * Checks if this combination can be calculated
         */
        internal bool CanBeCalculated()
        {
            return (GetNotSetCount() <= 1);
        }


        /*
         * Returns if the property has state NotSet
         */
        private bool _notSet(PropertyInfo<UnitValue> property)
        {
            return _propertyManager.GetState(property) == StateManager.State.NotSet;
        }


        internal int GetSetCount()
        {
            int setCount = 0;
            if (_set(_properties[0])) setCount++;
            if (_set(_properties[1])) setCount++;
            if (_set(_properties[2])) setCount++;
            return setCount;
        }

        /*
        * Returns if the property has state NotSet
        */
        private bool _set(PropertyInfo<UnitValue> property)
        {
            return _propertyManager.GetState(property) == StateManager.State.User;
        }
        #endregion

        #region Increments Operations

        /*
         * Gets the value from a given incrementstep
         */
        internal decimal GetIncrementValue(int index, decimal incrementStep, decimal substanceIncrement)
        {
            decimal increment = _propertyManager.GetFactor(GetAt(index)).GetIncrementValue(incrementStep, substanceIncrement);
            return increment;
        }

        /*
         * Get the incrementstep for a given property (index)
         */
        internal decimal GetIncrementStep(int index, decimal value, decimal substanceIncrement)
        {
            Factor propertyFactor = _propertyManager.GetFactor(GetAt(index));
            decimal increment = propertyFactor.GetIncrementStep(value, substanceIncrement);
            return increment;
        }
        /*
         * Get the incrementstep for a given property (index)
         */
        internal decimal GetIncrementStep(int index, decimal substanceIncrement)
        {
            Factor propertyFactor = _propertyManager.GetFactor(GetAt(index));
            decimal increment = propertyFactor.GetIncrementStep(substanceIncrement);
            return increment;
        }
        public ICalculationCombination FindCombinationByProperty(PropertyInfo<UnitValue> property)
        {
            return (
                from i in _propertyManager.propertyCombinationList
                where
                    i.Contains(property)
                select
                    i

            ).FirstOrDefault<ICalculationCombination>();
        }

        #endregion
    }
}
