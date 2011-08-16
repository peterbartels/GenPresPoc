using System;
using System.Linq.Expressions;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.Business.Allowance
{
    public class PrescriptionPropertySetAllowance
    {
        public void DetemineCanBeSet(Prescription prescription)
        {
            if(prescription.Drug.Generic != "" && prescription.Drug.Route != "" && prescription.Drug.Shape != "")
            {
                prescription.Quantity.CanBeSet = true;
                prescription.Drug.Components[0].Substances[0].Quantity.CanBeSet = true;
                prescription.Drug.Quantity.CanBeSet = true;
            }
        }
    }
}
