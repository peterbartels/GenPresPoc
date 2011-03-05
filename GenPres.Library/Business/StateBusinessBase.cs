using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.Linq;
using Csla;
using Csla.Data;
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
    [Serializable()]
    public abstract class StateBusinessBase<T> : DataBusinessBase<T>, ICalcalutorBusiness
        where T : StateBusinessBase<T> 
    {
        [NonSerialized]
        private Dictionary<PropertyInfo<UnitValue>, StateManager.ValueState> PropertyStates = new Dictionary<PropertyInfo<UnitValue>, StateManager.ValueState>();
        
        [NonSerialized]
        protected StateManager.ValueState DefaultState = StateManager.ValueState.User;


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
                if (GetProperty(propertyName).UIState == "Calculated" && !PropertyStates.Keys.Contains<PropertyInfo<UnitValue>>(propertyName))
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


                if (GetProperty(propertyName).UIState == "Calculated")
                {
                    PropertyStates[propertyName] = StateManager.ValueState.Calculated;
                    return StateManager.ValueState.Calculated;
                }

                if (GetProperty(propertyName).UIState == "User")
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
                    GetProperty(propertyName).UIState = "NotSet";

                if (state == StateManager.ValueState.User)
                    GetProperty(propertyName).UIState = "User";

                if (state == StateManager.ValueState.Calculated)
                    GetProperty(propertyName).UIState = "Calculated";
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
                return DataPortal.Create<UnitValue>();
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
