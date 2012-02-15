using System;
using System.Linq.Expressions;
using Informedica.GenPres.Business.Calculation.Old;
using Informedica.GenPres.Business.Calculation.Old.Combination;
using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Business.Domain.Units;
using Informedica.GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenPres.xTest.Calculation.CalculatorTests
{
    [TestClass]
    public class CalculationTest : BaseGenPresTest
    {
        public static string _testPath = @"c:\temp\test.txt";

        [TestMethod]
        public void CalculatorSetsIncrements()
        {
            var prescription = CreateParacetamolRect(Prescription.NewPrescription());
            var pc = new OldPrescriptionCalculator(prescription);
            Assert.IsTrue(prescription.Frequency.Factor.IncrementSizes.Length > 0);
            Assert.IsTrue(prescription.Quantity.Factor.IncrementSizes.Length > 0);
            Assert.IsTrue(prescription.Total.Factor.IncrementSizes.Length > 0);
        }

        public void AddCombiToCalculator(Prescription p, OldPrescriptionCalculator pc, params Expression<Func<UnitValue>>[] properties)
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
            var pc = new OldPrescriptionCalculator(prescription);
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
            var pc = new OldPrescriptionCalculator(prescription);
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
            var pc = new OldPrescriptionCalculator(prescription);
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
            prescription.Doses[0].Total.Value = 480;
            var pc = new OldPrescriptionCalculator(prescription);
            AddCombiToCalculator(prescription, pc, () => prescription.Doses[0].Total, () => prescription.Frequency, () => prescription.Doses[0].Quantity);
            pc.Execute();
            pc.Finish();
            Assert.AreEqual(240, prescription.Doses[0].Quantity.Value, "wrong dose quantity value");
        }

        [TestMethod]
        public void CalculatorCanCalculateDoseTotal()
        {
            var prescription = CreateParacetamolRect(Prescription.NewPrescription());
            prescription.Frequency.Value = 2;
            prescription.Doses[0].Quantity.Value = 480;
            var pc = new OldPrescriptionCalculator(prescription);
            AddCombiToCalculator(prescription, pc, () => prescription.Doses[0].Total, () => prescription.Frequency, () => prescription.Doses[0].Quantity);
            pc.Execute();
            pc.Finish();
            Assert.AreEqual(960, prescription.Doses[0].Total.Value, "wrong dose total value");
        }
        
        [TestMethod]
        public void CalculatorCanRectifySimpleCalculation()
        {
            var prescription = CreateParacetamolRect(Prescription.NewPrescription());
            prescription.Frequency.Value = 2;
            prescription.Quantity.Value = 4;
            var pc = new OldPrescriptionCalculator(prescription);
            
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
        public void CannotSetStatesOfCombination()
        {
            var prescription = Prescription.NewPrescription();
            prescription.Total.Unit = "zetp";
            prescription.Total.Value = 2;
            prescription.Quantity.Unit = "zetp";
            prescription.Quantity.Value = 1;
            prescription.Frequency.Value = 2;

            OldPrescriptionCalculator pc = new OldPrescriptionCalculator(prescription);
            pc.CheckStates();

            Assert.IsTrue(prescription.Total.UIState == "calculated");
            Assert.IsTrue(prescription.Frequency.UIState == "user");
            Assert.IsTrue(prescription.Quantity.UIState == "user");
        }
    }
}
