using System;
using System.IO;
using GenPres.Business.Calculation;
using GenPres.Business.Calculation.Combination;
using GenPres.Business.Domain;
using GenPres.Business.Domain.Prescriptions;
using GenPres.xTest.Base;
using GenPres.xTest.Calculation.Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Business.Calculator
{
    [TestClass]
    public class CombinationTest : BaseGenPresTest
    {
        public static string _testPath = @"c:\temp\test.txt";

        private static IPrescription GetTestPrescription()
        {
            var prescription = ObjectCreator.New<IPrescription>();
            prescription.Drug.Generic = "paracetamol";
            prescription.Drug.Route = "rect";
            prescription.Drug.Shape = "zetp";

            prescription.Frequency.Value = 2;
            prescription.Frequency.Time = "dag";

            prescription.Quantity.Unit = "zetp";
            
            prescription.Total.Unit = "zetp";
            prescription.Total.Time = "dag";
            prescription.Total.Value = 7;
            return prescription;
        }

        [TestMethod]
        public void CalculatorSetsIncrements()
        {
            IPrescription prescription = GetTestPrescription();
            var pc = new PrescriptionCalculator(prescription);
            Assert.IsTrue(prescription.Frequency.Factor.IncrementSizes.Length > 0);
            Assert.IsTrue(prescription.Quantity.Factor.IncrementSizes.Length > 0);
            Assert.IsTrue(prescription.Total.Factor.IncrementSizes.Length > 0);
        }

        [TestMethod]
        public void CalculatorCanCalculateSimpleCalculation()
        {
            IPrescription prescription = GetTestPrescription();
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
            IPrescription prescription = GetTestPrescription();
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
            var prescription = GetTestPrescription();

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
