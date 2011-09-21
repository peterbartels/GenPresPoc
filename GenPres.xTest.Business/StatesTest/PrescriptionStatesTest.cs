using System;
using System.Linq.Expressions;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using GenPres.Business.Calculation;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Business.Domain.Units;
using GenPres.Business.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Business.StatesTest
{
    [TestClass]
    public class PrescriptionStatesTest
    {
        [TestMethod]
        public void CannotSetStatesOfCombination()
        {
            var prescription = Prescription.NewPrescription();
            prescription.Total.Unit = "zetp";
            prescription.Total.Value = 2;
            prescription.Quantity.Unit = "zetp";
            prescription.Quantity.Value = 1;
            prescription.Frequency.Value = 2;
            
            PrescriptionCalculator pc = new PrescriptionCalculator(prescription);
            pc.CheckStates();

            Assert.IsTrue(prescription.Total.UIState == "calculated");
            Assert.IsTrue(prescription.Frequency.UIState == "user");
            Assert.IsTrue(prescription.Quantity.UIState == "user");
        }
    }
}
