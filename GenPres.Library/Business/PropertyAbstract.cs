using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business
{
    [Serializable]
    public abstract class PropertyAbstract
    {
        protected Prescription prescription;
        public List<Csla.PropertyInfo<UnitValue>> Combination;

        /// <summary>
        /// Uses polymorhpism to get the business object
        /// </summary>
        /// <param name="name"></param>
        /// <param name="iComponent"></param>
        /// <param name="iSubstance"></param>
        /// <returns></returns>
        internal ICalcalutorBusiness GetObject(Csla.PropertyInfo<UnitValue> name, int iDose)
        {
            if (name == Prescription.FrequencyProperty 
                || name == Prescription.QuantityProperty
                || name == Prescription.TotalProperty
                || name == Prescription.RateProperty
                || name == Prescription.TimeProperty
                ) return prescription;

            if (name == Component.QuantityProperty
                || name == Component.DrugConcentrationProperty
                ) return prescription.Doses[iDose].Substance.GetComponent();

            if (name == Substance.ComponentConcentrationProperty
                || name == Substance.DrugConcentrationProperty
                || name == Substance.QuantityProperty
                ) return prescription.Doses[iDose].Substance;

            if (name == Dose.QuantityProperty
                || name == Dose.TotalProperty
                || name == Dose.RateProperty
                ) return prescription.Doses[iDose];

            if (name == Drug.QuantityProperty) return prescription.Drug;

            if (name == Medicine.ComponentIncrementProperty
                || name == Medicine.DoseIncrementProperty
                || name == Medicine.QuantityProperty
                ) return prescription.Medicine;

            throw new Exception("Property defined but not found in GetObject: " + name.FriendlyName);
        }

        internal StateManager.ValueState GetState(Csla.PropertyInfo<UnitValue> name, int iDose)
        {
            ICalcalutorBusiness obj = (ICalcalutorBusiness)GetObject(name, iDose);
            if (obj == null)
            {
                throw new Exception("Not found " + this.ToString() + "." + name.ToString());
            }
            return obj.GetState(name);
        }

        internal UnitValue GetUnitValue(Csla.PropertyInfo<UnitValue> name, int iDose)
        {
            ICalcalutorBusiness obj = (ICalcalutorBusiness)GetObject(name, iDose);
            return obj.GetUnitValue(name);
        }

        internal void SetState(Csla.PropertyInfo<UnitValue> name, int iDose, StateManager.ValueState state)
        {
            ICalcalutorBusiness obj = (ICalcalutorBusiness)GetObject(name, iDose);
            obj.SetState(name, state);
        }

        internal decimal GetBaseValue(Csla.PropertyInfo<UnitValue> name, int iDose)
        {
            ICalcalutorBusiness obj = GetObject(name, iDose);
            return obj.GetValue(name);
        }

        internal Prescription GetPrescription()
        {
            return prescription;
        }

        internal void SetBaseValue(Csla.PropertyInfo<UnitValue> name, decimal value, int iDose)
        {
            ICalcalutorBusiness obj = (ICalcalutorBusiness)GetObject(name, iDose);
            obj.SetValue(name, value);
            if (GetState(name, iDose) == StateManager.ValueState.NotSet) SetState(name, iDose, StateManager.ValueState.Calculated);
        }
        /// <summary>
        /// Loops through all objects to get state count
        /// </summary>
        /// <param name="state"></param>
        /// <param name="iComponent"></param>
        /// <param name="iSubstance"></param>
        /// <returns></returns>
        internal int GetStateCount(StateManager.ValueState state, int iDose)
        {
            int count = 0;

            for (int i = 0; i < Combination.Count; i++)
            {
                StateManager.ValueState checkState = GetState(Combination[i], iDose);
                if (checkState == state) count++;
            }

            return count;
        }
    }
}
