using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Calculation;
using GenPres.Business.Calculation.Combination;
using GenPres.Business.Domain;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.xTest.Calculation.Calculator
{
    public static class PrescriptionTestFunctions
    {
        public static Prescription GetTestPrescription()
        {
            var prescription = Prescription.NewPrescription();
            prescription.Drug.Generic = "paracetamol";
            prescription.Drug.Route = "rect";
            prescription.Drug.Shape = "zetp";

            prescription.Frequency.Value = 2;
            prescription.Frequency.Time = "dag";

            prescription.Quantity.Unit = "zetp";

            prescription.Total.Unit = "zetp";
            prescription.Total.Time = "dag";
            prescription.Total.Value = 7;

            prescription.Drug.Quantity.Unit = "zetp";
            prescription.Drug.Quantity.Value = 2;

            prescription.Drug.Components[0].Substances[0].Quantity.Value = 0.4m;
            prescription.Drug.Components[0].Substances[0].Quantity.Unit = "mg";
            return prescription;
        }
        public static PrescriptionCalculator SetCombinations(Prescription prescription)
        {
            var pc = new PrescriptionCalculator(prescription);
            var combi = new MultiplierCombination(
                prescription,
                () => prescription.Total, () => prescription.Frequency, () => prescription.Quantity
            );
            
            var subtance = prescription.Drug.Components[0].Substances[0];
            var drug = prescription.Drug;

            var combi2 = new MultiplierCombination(
                prescription,
                () => drug.Quantity, () => subtance.Quantity, () => subtance.DrugConcentration
            );
            pc.AddCalculation(combi);
            return pc;
        }
    }
}
