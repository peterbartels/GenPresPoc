using GenPres.Business.Calculation;
using GenPres.Business.Calculation.Combination;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Calculation.Calculator
{
    [TestClass]
    public class CombinationTest : BaseGenPresTest
    {
        public static string _testPath = @"c:\temp\test.txt";

        [TestMethod]
        public void CalculatorSetsIncrements()
        {
            var prescription = PrescriptionTestFunctions.GetTestPrescription();
            var pc = new PrescriptionCalculator(prescription);
            Assert.IsTrue(prescription.Frequency.Factor.IncrementSizes.Length > 0);
            Assert.IsTrue(prescription.Quantity.Factor.IncrementSizes.Length > 0);
            Assert.IsTrue(prescription.Total.Factor.IncrementSizes.Length > 0);
        }

        [TestMethod]
        public void CalculatorCanCalculateSimpleCalculation()
        {
            var prescription = PrescriptionTestFunctions.GetTestPrescription();
            prescription.Total.Value = 8;
            var pc = new PrescriptionCalculator(prescription);

            var combi = new MultiplierCombination(
                prescription,
                () => prescription.Total, () => prescription.Frequency, () => prescription.Quantity
            );

            pc.AddCalculation(combi);
            pc.ExecuteCalculation();
            pc.FinishCalculation();

            Assert.AreEqual(2, prescription.Frequency.Value, "wrong frequency value");
            Assert.AreEqual(8, prescription.Total.Value, "wrong total value");
            Assert.AreEqual(4, prescription.Quantity.Value, "woring quantity value");
        }


        [TestMethod]
        public void CalculatorCanRectifySimpleCalculation()
        {
            var prescription = PrescriptionTestFunctions.GetTestPrescription();
            var pc = new PrescriptionCalculator(prescription);
            
            var combi = new MultiplierCombination(
                prescription,
                () => prescription.Total, () => prescription.Frequency, () => prescription.Quantity
            );

            pc.AddCalculation(combi);
            pc.ExecuteCalculation();
            pc.FinishCalculation();

            Assert.AreEqual(2, prescription.Frequency.Value, "wrong frequency value");
            Assert.AreEqual(8, prescription.Total.Value, "wrong total value");
            Assert.AreEqual(4, prescription.Quantity.Value, "woring quantity value");
            
        }

        [TestMethod]
        public void CalculatorCanCalculateUsingIncrements()
        {
            var prescription = PrescriptionTestFunctions.GetTestPrescription();

            var pc = new PrescriptionCalculator(prescription);
            
            var valid = true;

            var combi = new MultiplierCombination(
                prescription,
                () => prescription.Total, () => prescription.Frequency, () => prescription.Quantity
            );

            pc.AddCalculation(combi);
            
            for (var i = 0; i < 5000; i++)
            {
                CombinationRandomizer.RandomizeCombination(combi);
                pc.ConvertCombinationsValuesToArray();
                pc.ExecuteCalculation();
                Assert.IsTrue(combi.Validate());
            }
        }

        [TestMethod]
        public void RunAutomatedCalcSingle()
        {
            /*for (int i = 0; i < 5000; i++)
            {
                IPrescription prescription = GetTestPrescription();
                CalculatorCreator.SetRandomValue(prescription.Frequency, 1);
                CalculatorCreator.SetRandomValue(prescription.Total, 1);
                PrescriptionCalculator.Calculate(prescription);
                var calculatedResult = Math.Round(prescription.Total.BaseValue, 8, MidpointRounding.AwayFromZero);
                var expectedResult = Math.Round((prescription.Frequency.BaseValue * prescription.Quantity.BaseValue), 8, MidpointRounding.AwayFromZero);
                Assert.IsTrue(calculatedResult == expectedResult);
            }*/
        }
    }
}
