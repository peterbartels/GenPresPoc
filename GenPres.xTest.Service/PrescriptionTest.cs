using GenPres.Data.DTO.Prescriptions;
using GenPres.Data.Managers;
using GenPres.Service;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

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
            ObjectFactory.Configure(x => x.For<IDataContextManager>().Use<TestDataContextManager>());
        }

        [TestMethod]
        public void CanSavePrescription()
        {   
            var pDto = new PrescriptionDto();
            pDto.drugGeneric = "paracetamol";
            pDto.drugRoute = "rect";
            pDto.drugShape = "zetp";
            pDto.StartDate = "2011-07-01 12:00:00";
            pDto = PrescriptionService.SavePrescription(pDto, "8697898");
            Assert.IsTrue(pDto.Id > 0);
        }

        [TestMethod]
        public void CanGetPrescriptions()
        {
            var prescriptions = PrescriptionService.GetPrescriptions("8697898");
            Assert.IsTrue(prescriptions.Count > 0);
        }

        [TestMethod]
        public void CanGetPrescriptionById()
        {
            var prescriptions = PrescriptionService.GetPrescriptions("8697898");
            var lastPrescription = prescriptions[0];
            var prescription = PrescriptionService.GetPrescriptionById(lastPrescription.Id);
            Assert.IsTrue(prescription.PID != "" && prescription.Id > 0);
        }
    }
}
