using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using GenPres.Business.Calculation;
using GenPres.Business.Domain.Prescriptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Calculation
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
