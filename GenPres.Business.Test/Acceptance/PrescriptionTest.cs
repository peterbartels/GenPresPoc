using GenPres.Business.Data.Client.PrescriptionData;
using GenPres.Business.Service;
using GenPres.DataAccess;
using GenPres.xTest.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace GenPres.Business.Test.Acceptance
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
        public void _can_SavePrescription()
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
        public void _can_GetPrescriptions()
        {
            var prescriptions = PrescriptionService.GetPrescriptions("8697898");
            Assert.IsTrue(prescriptions.Count > 0);

        }
    }
}
