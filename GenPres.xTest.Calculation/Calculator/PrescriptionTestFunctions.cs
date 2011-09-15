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
