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
            prescription.Total.Value = 3;
            return prescription;
        }
        
        [TestMethod]
        public void CanSetCombinationValues()
        {
            var prescription = _getTestPrescription();
            PrescriptionCalculator._setIncrements(prescription);

            var prescriptionCalculate = new MultiplierCombination(
                prescription,
                () => prescription.Total, () => prescription.Frequency, () => prescription.Quantity
            );

            prescriptionCalculate.Calculate();
        }


    }
}
