using System;
using System.Linq.Expressions;
using GenPres.Business.Calculation;
using GenPres.Business.Calculation.Combination;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Business.Domain.Units;
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
            var prescription = CreateParacetamolRect(Prescription.NewPrescription());
            var pc = new PrescriptionCalculator(prescription);
            Assert.IsTrue(prescription.Frequency.Factor.IncrementSizes.Length > 0);
            Assert.IsTrue(prescription.Quantity.Factor.IncrementSizes.Length > 0);
            Assert.IsTrue(prescription.Total.Factor.IncrementSizes.Length > 0);
        }

        public void AddCombiToCalculator(Prescription p, PrescriptionCalculator pc, params Expression<Func<UnitValue>>[] properties)
        {
            var combi = new MultiplierCombination(p,properties);
            pc.AddCalculation(combi);
        }

        [TestMethod]
        public void CalculatorCanCalculateFrequency()
        {
            var prescription = CreateParacetamolRect(Prescription.NewPrescription());
            prescription.Total.Value = 8;
            prescription.Quantity.Value = 4;
            var pc = new PrescriptionCalculator(prescription);
            AddCombiToCalculator(prescription, pc, () => prescription.Total, () => prescription.Frequency, () => prescription.Quantity);
            pc.Execute();
            pc.Finish();
            Assert.AreEqual(2, prescription.Frequency.Value, "wrong prescription frequency value");
        }

        [TestMethod]
        public void CalculatorCanCalculatePrescriptionQuantity()
        {
            var prescription = CreateParacetamolRect(Prescription.NewPrescription());
            prescription.Frequency.Value = 2;
            prescription.Total.Value = 8;
            var pc = new PrescriptionCalculator(prescription);
            AddCombiToCalculator(prescription, pc, () => prescription.Total, () => prescription.Frequency, () => prescription.Quantity);
            pc.Execute();
            pc.Finish();
            Assert.AreEqual(4, prescription.Quantity.Value, "wrong admin quantity value");
        }

        [TestMethod]
        public void CalculatorCanCalculatePrescriptionTotal()
        {
            var prescription = CreateParacetamolRect(Prescription.NewPrescription());
            prescription.Frequency.Value = 2;
            prescription.Quantity.Value = 4;
            var pc = new PrescriptionCalculator(prescription);
            AddCombiToCalculator(prescription, pc, () => prescription.Total, () => prescription.Frequency, () => prescription.Quantity);
            pc.Execute();
            pc.Finish();
            Assert.AreEqual(8, prescription.Total.Value, "wrong admin total value");
        }

        [TestMethod]
        public void CalculatorCanCalculateDoseQuantity()
        {
            var prescription = CreateParacetamolRect(Prescription.NewPrescription());
            prescription.Frequency.Value = 2;
            prescription.Doses[0].Total.Value = 8;
            var pc = new PrescriptionCalculator(prescription);
            AddCombiToCalculator(prescription, pc, () => prescription.Doses[0].Total, () => prescription.Frequency, () => prescription.Doses[0].Quantity);
            pc.Execute();
            pc.Finish();
            Assert.AreEqual(4, prescription.Doses[0].Quantity.Value, "wrong dose quantity value");
        }

        [TestMethod]
        public void CalculatorCanCalculateDoseTotal()
        {
            var prescription = CreateParacetamolRect(Prescription.NewPrescription());
            prescription.Frequency.Value = 2;
            prescription.Doses[0].Quantity.Value = 4;
            var pc = new PrescriptionCalculator(prescription);
            AddCombiToCalculator(prescription, pc, () => prescription.Doses[0].Total, () => prescription.Frequency, () => prescription.Doses[0].Quantity);
            pc.Execute();
            pc.Finish();
            Assert.AreEqual(8, prescription.Doses[0].Total.Value, "wrong dose total value");
        }
        /*
        [TestMethod]
        public void CalculatorCanRectifySimpleCalculation()
        {
            var prescription = CreateParacetamolRect(Prescription.NewPrescription());
            var pc = new PrescriptionCalculator(prescription);
            
            var combi = new MultiplierCombination(
                prescription,
                () => prescription.Total, () => prescription.Frequency, () => prescription.Quantity
            );

            pc.AddCalculation(combi);
            pc.Execute();
            pc.Finish();

            Assert.AreEqual(2, prescription.Frequency.Value, "wrong frequency value");
            Assert.AreEqual(8, prescription.Total.Value, "wrong total value");
            Assert.AreEqual(4, prescription.Quantity.Value, "woring quantity value");
            
        }

        [TestMethod]
        public void CalculatorCanCalculateUsingIncrements()
        {
            var prescription = CreateParacetamolRect(Prescription.NewPrescription());

            var pc = new PrescriptionCalculator(prescription);
            
            var valid = true;

            var combi1 = new MultiplierCombination(
                prescription,
                () => prescription.Total, () => prescription.Frequency, () => prescription.Quantity
            );

            GenPres.Business.Domain.Prescriptions.Dose d = prescription.Doses[0];

            var combi2 = new MultiplierCombination(
                prescription,
                () => d.Total, () => prescription.Frequency, () => d.Quantity
            );

            pc.AddCalculation(combi1);
            pc.AddCalculation(combi2);
            
            for (var i = 0; i < 5000; i++)
            {
                CombinationRandomizer.RandomizeCombination(combi1);
                pc.ConvertCombinationsValuesToArray();
                pc.Execute();
                Assert.IsTrue(combi1.Validate());
                Assert.IsTrue(combi2.Validate());
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
            }
        }*/
    }
}
