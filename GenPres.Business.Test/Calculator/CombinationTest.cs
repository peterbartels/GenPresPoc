using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using GenPres.Business.Domain;
using GenPres.Business.Domain.Calculation;
using GenPres.Business.Domain.Calculation.Combination;
using GenPres.Business.Domain.Prescriptions;
using GenPres.xTest.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.Business.Test.Calculator
{
    [TestClass]
    public class CombinationTest : BaseGenPresTest
    {
        private string _testPath = @"c:\temp\test.txt";

        private IPrescription _getTestPrescription()
        {
            var prescription = ObjectFactory.New<IPrescription>();
            prescription.Frequency.Value = 2;
            prescription.Frequency.Time = "dag";

            prescription.Quantity.Unit = "zetp";
            
            prescription.Total.Unit = "zetp";
            prescription.Total.Time = "dag";
            prescription.Total.Value = 7;
            return prescription;
        }
        
        [TestMethod]
        public void CanSetCombinationValues()
        {
            IPrescription prescription = _getTestPrescription();
            PrescriptionCalculator.Calculate(prescription);
            
            Assert.AreEqual(2, prescription.Frequency.Value, "wrong frequency value");
            Assert.AreEqual(8, prescription.Total.Value, "wrong total value");
            Assert.AreEqual(4, prescription.Quantity.Value, "woring quantity value");
        }

        [TestMethod]
        public void RunAutomatedCalcSingle()
        {
            for (int i = 0; i < 5000; i++)
            {
                IPrescription prescription = _getTestPrescription();
                CalculatorCreator.SetRandomValue(prescription.Frequency, 1);
                CalculatorCreator.SetRandomValue(prescription.Total, 1);
                PrescriptionCalculator.Calculate(prescription);
                var calculatedResult = Math.Round(prescription.Total.BaseValue, 8, MidpointRounding.AwayFromZero);
                var expectedResult = Math.Round((prescription.Frequency.BaseValue * prescription.Quantity.BaseValue), 8, MidpointRounding.AwayFromZero);
                Assert.IsTrue(calculatedResult == expectedResult);
            }
        }
    }
}
