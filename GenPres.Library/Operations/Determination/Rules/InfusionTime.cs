using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using GenPres.Operations;
using Csla;
using GenPres.Business;

namespace GenPres.Operations.Determination.Rules
{
    class InfusionTime : IDetermination
    {
        public bool Determine(Prescription prescription)
        {
            /*PropertyManager propertyManager = new PropertyManager(prescription);
            if (propertyManager.GetState(Prescription.TimeProperty, 0) == StateManager.ValueState.User)
            {
                decimal timeValue = propertyManager.GetUnitValue(Prescription.TimeProperty, 0).BaseValue;
                if (propertyManager.GetState(Prescription.QuantityProperty, 0) == StateManager.ValueState.User)
                {
                    propertyManager.GetUnitValue(Prescription.RateProperty,0).BaseValue = propertyManager.GetUnitValue(Prescription.QuantityProperty,0).BaseValue / timeValue;
                    propertyManager.SetState(Prescription.TimeProperty, 0, StateManager.ValueState.NotSet);
                    propertyManager.SetState(Prescription.RateProperty, 0, StateManager.ValueState.User);
                    
                }

                if (propertyManager.GetState(Prescription.RateProperty, 0) == StateManager.ValueState.User)
                {
                    propertyManager.GetUnitValue(Prescription.QuantityProperty, 0).BaseValue = propertyManager.GetUnitValue(Prescription.RateProperty, 0).BaseValue * timeValue;
                    propertyManager.SetState(Prescription.TimeProperty, 0, StateManager.ValueState.NotSet);
                    propertyManager.SetState(Prescription.QuantityProperty, 0, StateManager.ValueState.User);
                }

            }*/
            return true;
        }
    }
}
