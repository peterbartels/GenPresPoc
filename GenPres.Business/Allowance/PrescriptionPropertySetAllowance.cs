using System;
using System.Linq.Expressions;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Business.Domain.Units;
using System.Collections.Generic;
using GenPres.Business.Allowance.Scenarios;

namespace GenPres.Business.Allowance
{
    public class PrescriptionPropertySetAllowance
    {
        public void DetemineCanBeSet(Prescription prescription)
        {
            if(prescription.Drug.Generic != "" && prescription.Drug.Route != "" && prescription.Drug.Shape != "")
            {
                var dose = prescription.Doses[0];
                var drug = prescription.Drug;
                var substance = prescription.Drug.Components[0].Substances[0];

                //var scenarios = new List<PropertyAllowanceConfig>[13];
                //scenarios[0] = new List<PropertyAllowanceConfig>(){

                prescription.Drug.Components[0].Substances[0].Quantity.CanBeSet = true;
                prescription.Quantity.CanBeSet = true;
                prescription.Total.CanBeSet = true;

                prescription.Frequency.CanBeSet = true;

                prescription.Doses[0].Quantity.CanBeSet = true;
                prescription.Doses[0].Total.CanBeSet = true;

                //prescription.Drug.Quantity.CanBeSet = true;

                if(prescription.Solution)
                {
                    prescription.Drug.Components[0].Substances[0].DrugConcentration.CanBeSet = true;
                    prescription.Drug.Quantity.CanBeSet = true;
                    if(prescription.Continuous)
                    {
                        prescription.Quantity.CanBeSet = false;
                        prescription.Total.CanBeSet = false;

                        prescription.Frequency.CanBeSet = false;

                        prescription.Doses[0].Quantity.CanBeSet = false;
                        prescription.Doses[0].Total.CanBeSet = false;

                        prescription.Doses[0].Rate.CanBeSet = true;
                        prescription.Rate.CanBeSet = true;
                    }
                }
            }
        }
    }
}
