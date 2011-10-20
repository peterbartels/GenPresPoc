using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using GenPres.Business.Allowance;
using GenPres.Business.Domain.Prescriptions;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace GenPres.xTest.Business.VisibilityTest
{
    /// <summary>
    /// Summary description for CanBeSetOfPrescriptionPropertiesTests
    /// </summary>
    [TestClass]
    public class CanBeSetOfPrescriptionPropertiesTests : BaseGenPresTest
    {
        [TestMethod]
        public void WhenDrugHasNoGenericSubstanceQuantityCanNotBeSet()
        {
            var prescription = CreateNoVolumes();
            prescription.Drug.Generic = "";
            PrescriptionAllowance.Determine(prescription);
            Assert.IsTrue(!prescription.Drug.Quantity.CanBeSet);
        }

        
        [TestMethod]
        public void WhenDrugGenericRouteAndShapeIsSetSubstanceQuantityCanBeSet()
        {
            var prescription = CreateNoVolumes();
            prescription.Drug.Generic = "paracetamol";
            prescription.Drug.Route = "rect";
            prescription.Drug.Shape = "zetp";
            PrescriptionAllowance.Determine(prescription);
            Assert.IsTrue(prescription.FirstSubstance.Quantity.CanBeSet);
        }
    }

}
