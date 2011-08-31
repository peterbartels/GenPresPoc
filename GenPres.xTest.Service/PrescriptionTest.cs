using System;
using GenPres.Data.DTO;
using GenPres.Data.DTO.Prescriptions;
using GenPres.Service;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Service
{
    [TestClass]
    public class PrescriptionTest : BaseGenPresTest
    {
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

        public PrescriptionTest()
        {
            
        }
        
        [TestMethod]
        public void CanSavePrescription()
        {   
            var pDto = new PrescriptionDto();
            pDto.drugGeneric = "paracetamol";
            pDto.drugRoute = "rect";
            pDto.drugShape = "zetp";
            pDto.startDate = "2011-07-01 12:00:00";
            pDto.substanceQuantity = new UnitValueDto();
            pDto.substanceQuantity.unit = "mg";
            pDto = PrescriptionService.SavePrescription(pDto, "8697898");
            Assert.IsTrue(pDto.Id != Guid.Empty.ToString());
        }
        
        [TestMethod]
        public void CanGetPrescriptions()
        {
            var prescriptions = PrescriptionService.GetPrescriptions("8697898");
            Assert.IsTrue(prescriptions.Count > 0);
        }
        /*
        [TestMethod]
        public void CanGetPrescriptionById()
        {
            var prescriptions = PrescriptionService.GetPrescriptions("8697898");
            var lastPrescription = prescriptions[0];
            var prescription = PrescriptionService.GetPrescriptionById(lastPrescription.Id);
            Assert.IsTrue(prescription.PID != "" && prescription.Id > 0);
        }*/
    }
}
