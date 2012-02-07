using GenPres.Business.Calculation.Old;
using GenPres.Business.Calculation.Old.Combination;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.xTest.Calculation.CalculatorTests
{
    public static class PrescriptionTestFunctions
    {
        public static OldPrescriptionCalculator SetCombinations(Prescription prescription)
        {
            var pc = new OldPrescriptionCalculator(prescription);
            var combi = new MultiplierCombination(
                prescription,
                () => prescription.Total, () => prescription.Frequency, () => prescription.Quantity
            );
            
            pc.AddCalculation(combi);
            return pc;
        }
    }
}
