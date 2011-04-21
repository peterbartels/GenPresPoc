using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.Business.Service;
using GenPres.Business.Data;
using GenPres.Business.Domain;
using TypeMock;
using TypeMock.ArrangeActAssert;

namespace GenPres.Test.Unit
{
    [TestClass]
    public class PrescriptionTest
    {
        #region Constructor
        public PrescriptionTest()
        { }
        #endregion

        #region TestContext
        private TestContext testContextInstance;
        
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        #endregion
        /*
        [TestMethod]
        public void NewPrescription_returns_DTO()
        {
            NewPrescriptionDto testDto = PrescriptionService.NewPrescription();
            Assert.IsTrue(testDto != null);
        }

        [TestMethod]
        public void Prescription_factory_calls_setDefaults()
        {
            Isolate.NonPublic.WhenCalled<Prescription>("setDefaults");
            var test = DataPortal.Create<Prescription>();
            Isolate.Verify.NonPublic.WasCalled(typeof(Prescription), "setDefaults");
        }

        [TestMethod]
        public void Prescription_setDefaults()
        {
            Prescription newPrescription = DataPortal.Create<Prescription>();
            Assert.IsTrue(newPrescription.Components.Count == 1);
            Assert.IsTrue(newPrescription.Components[0].Substances.Count == 1);

            Assert.IsTrue(newPrescription.Quantity != null);
            Assert.IsTrue(newPrescription.Components[0].Quantity != null);
            Assert.IsTrue(newPrescription.Components[0].Substances[0].Quantity != null);
        }

        [TestMethod]
        public void Prescription_TestNewPrescriptionDTO()
        {
            Prescription newPrescription = DataPortal.Create<Prescription>();
            NewPrescriptionAssembler prescriptionAssembler = new NewPrescriptionAssembler();
            prescriptionAssembler.MapToDto(newPrescription, new NewPrescriptionDto());
        }
        */
        [TestMethod]
        public void Prescription_can_GetPrescriptionById()
        {
            
        }
    }
}
