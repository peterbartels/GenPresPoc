using GenPres.Business.Service;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Service
{
    [TestClass]
    public class MedicineTest : BaseGenPresTest
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
        public void CanGetGenerics()
        {
            var generics = MedicineService.GetGenerics("", "");
            Assert.IsTrue(generics.Count > 0, "No generics found");
        }

        [TestMethod]
        public void CanGetRoutes()
        {
            var generics = MedicineService.GetRoutes("", "");
            Assert.IsTrue(generics.Count > 0, "No generics found");
        }

        [TestMethod]
        public void CanGetShapes()
        {
            var generics = MedicineService.GetShapes("", "");
            Assert.IsTrue(generics.Count > 0, "No generics found");
        }
    }
}
