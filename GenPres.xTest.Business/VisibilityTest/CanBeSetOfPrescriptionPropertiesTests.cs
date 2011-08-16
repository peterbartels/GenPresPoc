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
        public void WhenDrugHasNoGenericSubstanceQuantityCanBeSet()
        {
            var prescription = CreatePrescriptionWithAllPropertiesSet();
            prescription.Drug.Generic = "";
            Assert.IsTrue(!prescription.Drug.Quantity.CanBeSet);
        }

        [TestMethod]
        public void WhenDrugGenericIsSetDetemineCanBeSetIsCalled()
        {
            var prescription = CreatePrescriptionWithAllPropertiesSet();
            var setter = Isolate.Fake.Instance<PrescriptionPropertySetAllowance>();
            Drug.PrescriptionAllowance = setter;
            prescription.Drug.Generic = "paracetamol";
            Isolate.Verify.WasCalledWithAnyArguments(() => setter.DetemineCanBeSet(prescription));
        }

        [TestMethod]
        public void WhenDrugGenericRouteAndShapeIsSetSubstanceQuantityCanBeSet()
        {
            var prescription = CreatePrescriptionWithAllPropertiesSet();
            prescription.Drug.Generic = "paracetamol";
            prescription.Drug.Route = "rect";
            prescription.Drug.Shape = "zetp";
            Assert.IsTrue(prescription.Drug.Quantity.CanBeSet);
        }
    }

}
