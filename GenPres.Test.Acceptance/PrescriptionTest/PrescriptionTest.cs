using GenPres.Business.Service;
using GenPres.xTest.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.Test.Acceptance.PrescriptionTest
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

        [TestMethod]
        public void _can_SavePrescription()
        {

        }
        [TestMethod]
        public void _can_GetPrescriptions()
        {
            var prescriptions = PrescriptionService.GetPrescriptions();

        }
    }
}
