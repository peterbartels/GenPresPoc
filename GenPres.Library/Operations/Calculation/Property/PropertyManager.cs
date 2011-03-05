using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business;
using Csla;

namespace GenPres.Operations.Calculation
{
    public class PropertyManager : PropertyAbstract
    {
        private int _currentDose = 0;
        public List<ICalculationCombination> propertyCombinationList;
        public List<ICalculationCombination> incrementCombinationList;

        #region Singleton
        static PropertyManager instance = null;
        static readonly object padlock = new object();
        public PropertyManager() {
        
        }

        public static PropertyManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance==null)
                    {
                        instance = new PropertyManager();
                    }
                    return instance;
                }
            }
        }
        #endregion

        #region General Get and Set Properties
        public void SetPrescription(Prescription prescription)
        {
            base.prescription = prescription;
        }

        public void SetCurrentDose(int dose) {
            _currentDose = dose;
        }

        public bool IsPrescriptionSolution()
        {
            return prescription.Solution;
        }
        #endregion

        #region Value management
        internal void SetValue(PropertyInfo<UnitValue> name, decimal value)
        {
            if (name == Prescription.QuantityProperty)
            {
                prescription.Quantity.BaseValue = value;
                return;
            }
                
            if(name == Prescription.TotalProperty){
                decimal result = value * UnitConverter.GetUnitValue(prescription.Frequency.Time, 1);
                prescription.Total.BaseValue = result;
                return;
            }
            
            if(name == Prescription.FrequencyProperty) {
                prescription.Frequency.Value = value;
                return;
            }
            if(name == Dose.QuantityProperty) {
                prescription.Doses[_currentDose].Quantity.BaseValue = value;
                return;
            }
            if(name == Dose.TotalProperty) {
                decimal valResult = value * UnitConverter.GetUnitValue(prescription.Frequency.Time, 1);
                prescription.Doses[_currentDose].Total.BaseValue = valResult;
                return;
            }
            if(name == Dose.RateProperty) {
                decimal doseRate = value * UnitConverter.GetUnitValue("uur", 1);
                prescription.Doses[_currentDose].Rate.BaseValue = doseRate;
                return;
            }
            if(name == Prescription.RateProperty) {
                decimal prRate = value * UnitConverter.GetUnitValue("uur", 1);
                prescription.Rate.BaseValue = prRate;
                return;
            }
            if(name == Prescription.TimeProperty) {
                decimal duration = UnitConverter.GetBaseValue("uur", value);
                prescription.Time.BaseValue = duration;
                return;
            }
            if(name == Substance.DrugConcentrationProperty) {
                prescription.Doses[_currentDose].Substance.DrugConcentration.BaseValue = value;
                return;
            }
            if (name == Drug.QuantityProperty)
            {
                prescription.Drug.Quantity.BaseValue = value;
                return;
            }

            if (name == Component.QuantityProperty)
            {
                prescription.Doses[_currentDose].Substance.GetComponent().Quantity.BaseValue = value;
                return;
            }
            if(name == Substance.QuantityProperty) {
                prescription.Doses[_currentDose].Substance.Quantity.BaseValue = value;
                return;
            }
            if (name == Medicine.QuantityProperty)
            {
                prescription.Medicine.Quantity.Value = value;
                return;
            }
            if (name == Medicine.DoseIncrementProperty)
            {
                prescription.Medicine.DoseIncrement.Value = value;
                return;
            }
            if (name == Medicine.ComponentIncrementProperty)
            {
                prescription.Medicine.ComponentIncrement.Value = value;
                return;
            }

        }

        internal decimal GetValue(PropertyInfo<UnitValue> name)
        {
            if(name== Prescription.QuantityProperty)
                return prescription.Quantity.BaseValue;
            if(name== Prescription.TotalProperty)
                return prescription.Total.BaseValue / UnitConverter.GetUnitValue(prescription.Frequency.Time, 1);
            if(name== Prescription.RateProperty)
                return prescription.Rate.BaseValue / UnitConverter.GetUnitValue("uur", 1);
            if(name== Prescription.TimeProperty)
                return UnitConverter.GetUnitValue("uur", prescription.Time.BaseValue);
            if(name== Prescription.FrequencyProperty)
                return prescription.Frequency.Value;
            if(name== Dose.QuantityProperty)
                return prescription.Doses[_currentDose].Quantity.BaseValue;
            if(name == Substance.DrugConcentrationProperty)
                return prescription.Doses[_currentDose].Substance.DrugConcentration.BaseValue;
            if(name== Substance.QuantityProperty)
                return prescription.Doses[_currentDose].Substance.Quantity.BaseValue;
            if(name== Dose.TotalProperty)
                return prescription.Doses[_currentDose].Total.BaseValue / UnitConverter.GetUnitValue(prescription.Frequency.Time, 1);
            if(name== Dose.RateProperty)
                return prescription.Doses[_currentDose].Rate.BaseValue / UnitConverter.GetUnitValue("uur", 1);
            if(name== Drug.QuantityProperty)
                return prescription.Drug.Quantity.BaseValue;
            if (name == Component.QuantityProperty)
                return prescription.Doses[_currentDose].Substance.GetComponent().Quantity.BaseValue;
            if (name == Medicine.QuantityProperty)
                return prescription.Medicine.Quantity.Value;
            if (name == Medicine.ComponentIncrementProperty)
                return prescription.Medicine.ComponentIncrement.Value;
            if (name == Medicine.DoseIncrementProperty)
                return prescription.Medicine.DoseIncrement.Value;
            throw new Exception("Value not found:" + name);
        }


        internal bool TrySetValue(PropertyInfo<UnitValue> property, decimal setValue)
        {
            decimal checkValue = MathExt.FixPrecision(GetValue(property));
            decimal newValue = MathExt.FixPrecision(setValue);
            if (newValue != checkValue)
            {
                SetValue(property, newValue);
                if (GetState(property, _currentDose) == StateManager.ValueState.NotSet && GetState(property, _currentDose) != StateManager.ValueState.User) 
                    SetState(property, _currentDose, StateManager.ValueState.Calculated);
                return true;
            }
            return false;
        }


        internal void SetBaseValue(Csla.PropertyInfo<UnitValue> name, decimal value)
        {
            base.SetBaseValue(name, value, _currentDose);
        }
        #endregion

        #region State managemenr

        internal void SetState(Csla.PropertyInfo<UnitValue> name, StateManager.ValueState state)
        {
            base.SetState(name, _currentDose, state);
        }

        internal StateManager.ValueState GetState(Csla.PropertyInfo<UnitValue> name)
        {
            return base.GetState(name, _currentDose);
        }
        internal decimal GetBaseValue(Csla.PropertyInfo<UnitValue> name)
        {
            return base.GetBaseValue(name, _currentDose);
        }
        #endregion

        #region GetFactor, GetSubstanceIncrements, GetSequenceList
        internal Factor GetFactor(PropertyInfo<UnitValue> property)
        {
            return GetUnitValue(property, _currentDose).Factor;
        }

        internal decimal[] GetSubstanceIncrementValues()
        {
            return prescription.Doses[_currentDose].Substance.SubstanceIncrements;
        }

        internal decimal GetComponentIncrementValue()
        {
            return prescription.Doses[_currentDose].Substance.GetComponent().ComponentIncrement;
        }
        internal List<PropertyInfo<UnitValue>> GetSequenceList()
        {
            return PropertySequenceList.GetPropertySequence();
        }
        internal void CorrectPropertyValues()
        {
            foreach (AbstractCombination combination in propertyCombinationList)
            {
                combination.CorrectPropertyValues();
            }
            for (int i = 0; i < incrementCombinationList.Count; i++)
            {
                IncrementDetermination id = new IncrementDetermination(incrementCombinationList[i]);
                id.SetSingleValues();
            }
            for (int i = 0; i < incrementCombinationList.Count; i++)
            {
                IncrementDetermination id = new IncrementDetermination(incrementCombinationList[i]);
                if (incrementCombinationList[i].GetNotSetCount() < 3)
                {
                    id.DetermineIncrements();
                }
                
            }
        }
        #endregion

    }
}
