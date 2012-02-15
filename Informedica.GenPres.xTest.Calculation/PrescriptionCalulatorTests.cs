using Informedica.GenPres.Business.Calculation;
using Informedica.GenPres.Business.Domain.Prescriptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenPres.xTest.Calculation
{
    [TestClass]
    public class PrescriptionCalulatorTests
    {
        [TestMethod]
        public void ThatACalculatorCanBeInitialized()
        {
            var pc = new PrescriptionCalculator(Prescription.NewPrescription());
            Assert.IsNotNull(pc);
        }

        [TestMethod]
        public void ThatAFreqDosageCombinationCanBeMade()
        {
            Prescription prescription = Prescription.NewPrescription();
            var combi = new CalculationCombination(prescription, () => prescription.FirstDose.Total, () => prescription.Frequency, () => prescription.FirstDose.Quantity);
            Assert.IsNotNull(combi);
        }
    }
}
