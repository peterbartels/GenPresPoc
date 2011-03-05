using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using GenPres.Operations;
using Csla;
using GenPres.Operations.Calculation;
using GenPres.Business;

//using GenPres.Calculator.Rectification;
namespace GenPres.Operations.Determination.Rules
{
    class Solution : IDetermination
    { 
        public bool Determine(Prescription prescription)
        {
            PropertyManager pm = PropertyManager.Instance;
            pm.SetPrescription(prescription);

            if (!prescription.Solution)
            {
                prescription.Drug.Quantity.Value = 1;
                string componentUnit = prescription.Drug.Quantity.Unit.ToLower();
                if (componentUnit == "ml" || componentUnit == "l")
                {
                    prescription.Drug.Quantity.BaseValue = prescription.Drug.Components[0].ComponentIncrement;
                }
                else
                {
                    pm.SetValue(Component.QuantityProperty, 1);
                    if (prescription.Drug.Components[0].Substances[0].SubstanceIncrements.Length == 1)
                    {
                        pm.SetValue(Substance.DrugConcentrationProperty, prescription.Drug.Components[0].Substances[0].SubstanceIncrements[0] / prescription.Drug.Components[0].ComponentIncrement);
                        pm.SetState(Substance.QuantityProperty, StateManager.ValueState.Calculated);
                        pm.SetState(Substance.DrugConcentrationProperty, StateManager.ValueState.User);
                        pm.SetState(Drug.QuantityProperty, StateManager.ValueState.User);
                    }
                }
                pm.SetState(Drug.QuantityProperty, StateManager.ValueState.User);
                
                if (pm.GetState(Substance.QuantityProperty) == StateManager.ValueState.User)
                {
                    //decimal increment = findIncrement(prescription.PrescriptionDoses[0].Substance.Quantity.BaseValue, prescription.Drug.Components[0].Substances[0].SubstanceIncrements, prescription.Drug.Components[0].ComponentIncrement);
                    //if (increment > 0) prescription.Drug.Components[0].Substances[0].SubstanceIncrements = new decimal[] { increment / prescription.Drug.Components[0].ComponentIncrement };
                }

                return true;
            }
            return false;
        }
        private decimal findIncrement(decimal value, decimal[] increments, decimal componentIncrement)
        {
            for (int i = 0; i < increments.Length; i++)
            {
                if (MathExt.FixPrecision(increments[i] / componentIncrement) == value) return value;
            }
            return 0;
        }
    }
}
