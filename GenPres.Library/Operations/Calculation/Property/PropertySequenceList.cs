using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business;

namespace GenPres.Operations.Calculation
{
    public class PropertySequenceList
    {
        /*
         * The property sequency determines which properties are corrected first
         * This is needed to adjust it to the expectations of the user
         */
        public static List<Csla.PropertyInfo<UnitValue>> GetPropertySequence()
        {
            List<Csla.PropertyInfo<UnitValue>> propertyRectificationSequence = new List<Csla.PropertyInfo<UnitValue>>();
            propertyRectificationSequence.Add(Dose.TotalProperty);
            propertyRectificationSequence.Add(Dose.QuantityProperty);
            propertyRectificationSequence.Add(Substance.DrugConcentrationProperty);
            propertyRectificationSequence.Add(Prescription.TimeProperty);
            propertyRectificationSequence.Add(Substance.QuantityProperty);
            propertyRectificationSequence.Add(Component.QuantityProperty);
            propertyRectificationSequence.Add(Dose.RateProperty);
            propertyRectificationSequence.Add(Drug.QuantityProperty);
            propertyRectificationSequence.Add(Dose.RateProperty);
            propertyRectificationSequence.Add(Prescription.TotalProperty);
            propertyRectificationSequence.Add(Prescription.QuantityProperty);
            propertyRectificationSequence.Add(Prescription.RateProperty);
            propertyRectificationSequence.Add(Medicine.DoseIncrementProperty);
            propertyRectificationSequence.Add(Medicine.QuantityProperty);
            propertyRectificationSequence.Add(Medicine.ComponentIncrementProperty);
            
            return propertyRectificationSequence;
        }
    }
}
