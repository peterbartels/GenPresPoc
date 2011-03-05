using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Csla;
using GenPres.Business;


/// <summary>
/// Interface for all business objects that have possible calculated fields
/// </summary>
public interface ICalcalutorBusiness
{
    GenPres.Business.StateManager.ValueState GetState(Csla.PropertyInfo<UnitValue> propertyName);
    void SetValue(Csla.PropertyInfo<UnitValue> propertyName, decimal value);
    decimal GetValue(Csla.PropertyInfo<UnitValue> propertyName);
    UnitValue GetUnitValue(Csla.PropertyInfo<UnitValue> propertyName);
    void SetState(Csla.PropertyInfo<UnitValue> propertyName, StateManager.ValueState state);
}
    

namespace GenPres.Business
{
    /// <summary>
    /// Class that provides calculationactions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public abstract class CalculationBusinessBase<T>  : Csla.BusinessBase<T>, ICalcalutorBusiness
     where T : CalculationBusinessBase<T> 
    {

        private Dictionary<PropertyInfo<UnitValue>, StateManager.ValueState> PropertyStates = new Dictionary<PropertyInfo<UnitValue>, StateManager.ValueState>();
        protected StateManager.ValueState DefaultState = StateManager.ValueState.User;


        public abstract string[] MappingProperties
        {
            get;
        }

        protected void ApplyMapping(object source, object destination)
        {
            Csla.Data.DataMap mapping = new Csla.Data.DataMap(source.GetType(), destination.GetType());
            for (int i = 0; i < MappingProperties.Length; i++)
            {
                mapping.AddPropertyMapping(MappingProperties[i], MappingProperties[i]);
            }
            Csla.Data.DataMapper.Map(source, destination, mapping);
        }

        /// <summary>
        /// After change, change state to default
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void PropertyHasChanged(string propertyName)
        {
            MarkDirty(true);
            var propertyNames = ValidationRules.CheckRules(propertyName);
            if (ApplicationContext.PropertyChangedMode == ApplicationContext.PropertyChangedModes.Windows)
                OnPropertyChanged(propertyName);
            else
                foreach (var name in propertyNames)
                    OnPropertyChanged(name);

        }

        /// <summary>
        /// Retrieve state
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public StateManager.ValueState GetState(Csla.PropertyInfo<UnitValue> propertyName)
        {
            if (propertyName.Type == typeof(UnitValue))
            {

                if (GetProperty(propertyName) == null)
                {
                    return StateManager.ValueState.NotSet;
                }
                if (GetProperty(propertyName).State == "Calculated" && !PropertyStates.Keys.Contains<PropertyInfo<UnitValue>>(propertyName))
                {
                    GetProperty(propertyName).BaseValue = 0.0m;
                }
                if (GetProperty(propertyName).BaseValue == 0.0m)
                {
                    PropertyStates[propertyName] = StateManager.ValueState.NotSet;
                    return StateManager.ValueState.NotSet;
                }
                if (PropertyStates.ContainsKey(propertyName))
                {
                    return PropertyStates[propertyName];
                }

                
                if (GetProperty(propertyName).State == "Calculated")
                {
                    PropertyStates[propertyName] = StateManager.ValueState.Calculated;
                    return StateManager.ValueState.Calculated;
                }

                if (GetProperty(propertyName).State == "User")
                {
                    PropertyStates[propertyName] = StateManager.ValueState.User;
                    return StateManager.ValueState.User;
                }
                if (GetProperty(propertyName).BaseValue > 0.0m)
                {
                    return StateManager.ValueState.User;
                }
            }
            return StateManager.ValueState.NotSet;
        }

        /// <summary>
        /// This methods sets the state for a certain UnitValue Property
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public void SetState(Csla.PropertyInfo<UnitValue> propertyName, StateManager.ValueState state)
        {
            PropertyStates[propertyName] = state;
            if (GetProperty(propertyName) != null)
            {
                if (state == StateManager.ValueState.NotSet)
                    GetProperty(propertyName).State = "NotSet";

                if (state == StateManager.ValueState.User)
                    GetProperty(propertyName).State = "User";

                if (state == StateManager.ValueState.Calculated)
                    GetProperty(propertyName).State = "Calculated";
            }
        }

        /// <summary>
        /// Get a value for a UnitValue PropertyInfo Object
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public decimal GetValue(Csla.PropertyInfo<UnitValue> propertyName)
        {
            if (GetProperty(propertyName) == null) 
            {
                return 0;
            }
            return GetProperty(propertyName).BaseValue;
        }

        /// <summary>
        /// Get a value for a UnitValue PropertyInfo Object
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public UnitValue GetUnitValue(Csla.PropertyInfo<UnitValue> propertyName)
        {
            if (GetProperty(propertyName) == null)
            {
                return new UnitValue();
            }
            return GetProperty(propertyName);
        }


        /// <summary>
        /// Set the value for a UnitValue Property
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public void SetValue(Csla.PropertyInfo<UnitValue> propertyName, decimal value)
        {
            if (GetProperty(propertyName) == null)
            {
                PropertyStates[propertyName] = StateManager.ValueState.Calculated;
                return;
            }
            GetProperty(propertyName).BaseValue = value;
        }
    }
}
