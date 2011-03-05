using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business;
using GenPres.Operations.Calculation;

namespace GenPres.Operations.Determination.Rules
{
    class SetStates : IDetermination
    {
        public bool Determine(Prescription prescription)
        {
            PropertyManager pm = PropertyManager.Instance;
            pm.SetPrescription(prescription);
            if (prescription.prepareSave)
            {
                pm.SetState(Substance.QuantityProperty, 0, StateManager.ValueState.User);
                if (prescription.Solution)
                {
                    if (prescription.Continuous)
                    {
                        pm.SetState(Drug.QuantityProperty, 0, StateManager.ValueState.User);
                        pm.SetState(Dose.RateProperty, 0, StateManager.ValueState.Calculated);
                        pm.SetState(Prescription.RateProperty, 0, StateManager.ValueState.User);
                    }
                    else
                    {
                        pm.SetState(Drug.QuantityProperty, 0, StateManager.ValueState.User);

                        pm.SetState(Dose.QuantityProperty, 0, StateManager.ValueState.Calculated);
                        pm.SetState(Dose.TotalProperty, 0, StateManager.ValueState.Calculated);
                        pm.SetState(Dose.RateProperty, 0, StateManager.ValueState.Calculated);

                        pm.SetState(Prescription.QuantityProperty, 0, StateManager.ValueState.User);
                        pm.SetState(Prescription.TotalProperty, 0, StateManager.ValueState.User);
                        pm.SetState(Prescription.RateProperty, 0, StateManager.ValueState.User);

                        pm.SetState(Prescription.FrequencyProperty, 0, StateManager.ValueState.Calculated);
                        pm.SetState(Prescription.TimeProperty, 0, StateManager.ValueState.Calculated);
                    }
                }
            }

            return false;
        }
    }
}
