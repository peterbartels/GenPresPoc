using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using GenPres.Business.Domain;
using GenPres.Business.Domain.Calculation;
using GenPres.Business.Domain.Calculation.Combination;
using GenPres.Business.Domain.PrescriptionDomain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.Business.Test.Calculator
{
    [TestClass]
    public class CombinationTest
    {
        private Prescription _getTestPrescription()
        {
            var prescription = ObjectFactory.New<Prescription>();
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
            Prescription prescription = _getTestPrescription();
            PrescriptionCalculator.Calculate(prescription);
            
            Assert.AreEqual(7, prescription.Frequency.Value, "wrong frequency value");
            Assert.AreEqual(7, prescription.Total.Value, "wrong total value");
            Assert.AreEqual(7, prescription.Quantity.Value, "woring quantity value");

        }


    }
}
