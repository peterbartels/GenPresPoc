using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using GenPres.Operations.Calculation;
using GenPres.Business;

namespace GenPres.Operations.Determination.Rules
{
    class IncrementDetermination
    {
        private IncrementDeterminationEntity[] IncrementEntities;
        private PropertyManager propertyMan;
        
        public IncrementDetermination(IncrementDeterminationEntity[] incrementEntities, Prescription p)
        {
            IncrementEntities = incrementEntities;

            propertyMan = PropertyManager.Instance;
            propertyMan.SetPrescription(p);
        }

        public void Execute()
        {
            for (int i = 0; i < IncrementEntities.Length; i++)
            {
                IncrementDeterminationEntity ie = IncrementEntities[i];
                Determine(ie);
            }
        }
        private void Determine(IncrementDeterminationEntity ie)
        {
            /*decimal[] increments = propertyMan.GetSubstanceIncrements();
            List<decimal> result = new List<decimal>();
            for (int i = 0; i < increments.Length; i++)
            {
                decimal resultVal = 0;
                for (int m = 0; m < ie.Multipliers.Length; m++)
                {
                    if (propertyMan.GetState(ie.Multipliers[m],0) != StateManager.ValueState.User) return;
                    decimal value = propertyMan.ToIncrementStep(ie.Multipliers[m], 0, increments[i]);
                    if (value > 0)
                    {
                        if (resultVal == 0) resultVal = value;
                        else
                        {
                            if(!ie.Divide)
                                resultVal = resultVal * value;
                            else
                                resultVal = resultVal / value;
                        }
                    }
                }
                if (resultVal > 0) result.Add(propertyMan.FromIncrementStep(ie.BaseProperty, resultVal, increments[i]));
            }
            if(result.Count > 0){
                propertyMan.GetUnitValue(ie.BaseProperty, 0).AllowIncrementStep = ie.AllowMultipliers;
                propertyMan.GetUnitValue(ie.BaseProperty, 0).Increments = result.ToArray();
                propertyMan.GetUnitValue(ie.BaseProperty, 0).UpdateIncrements(propertyMan.GetPrescription());
            }*/
        }
    }

    class IncrementDeterminationEntity
    {
        public PropertyInfo<UnitValue>[] Multipliers;
        public PropertyInfo<UnitValue> BaseProperty;
        public bool AllowMultipliers;
        public PropertyInfo<UnitValue> KAE;
        public bool Divide = false;
        public IncrementDeterminationEntity(PropertyInfo<UnitValue> baseProperty, PropertyInfo<UnitValue>[] multipliers, bool allowMultipliers)
        {
            Multipliers = multipliers;
            AllowMultipliers = allowMultipliers;
            BaseProperty = baseProperty;
        }

        public IncrementDeterminationEntity(PropertyInfo<UnitValue> baseProperty, PropertyInfo<UnitValue>[] multipliers, bool allowMultipliers, bool divide)
        {
            Multipliers = multipliers;
            AllowMultipliers = allowMultipliers;
            BaseProperty = baseProperty;
            Divide = divide;
        }
    }

}
