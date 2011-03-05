using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business;
using GenPres.Operations.Calculation;

namespace GenPres.Operations.Determination.Rules
{
    class Continuous : IDetermination
    {
        private Prescription prescription;
        public bool Determine(Prescription p)
        {
            prescription = p;
            PropertyManager pm = PropertyManager.Instance;
            pm.SetPrescription(prescription);

            if (p.Continuous & !p.Infusion)
            {
                p.Frequency.Time = "dag";
                p.Frequency.Value = 1;
                p.Time.Unit = "uur";
                p.Time.Value = 24;

                pm.SetState(Dose.QuantityProperty, 0, StateManager.ValueState.Calculated);
                pm.SetState(Dose.TotalProperty, 0, StateManager.ValueState.Calculated);
                pm.SetState(Prescription.QuantityProperty, 0, StateManager.ValueState.Calculated);
                pm.SetState(Prescription.TotalProperty, 0, StateManager.ValueState.Calculated);
                pm.SetState(Prescription.FrequencyProperty, 0, StateManager.ValueState.Calculated);
                pm.SetState(Prescription.TimeProperty, 0, StateManager.ValueState.Calculated);
                
            }
            return false;
        }
    }
}
