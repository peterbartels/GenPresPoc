using GenPres.Business.Service;
using GenPres.xTest.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.Test.Acceptance.MedicineTest
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
        public void _can_GetGenerics()
        {
            var generics = MedicineService.GetGenerics("", "");
            Assert.IsTrue(generics.Count > 0, "No generics found");
            //Assert.IsTrue(generics.Contains("paracetamol"), "A specific generic is not found.");
        }

        [TestMethod]
        public void _can_GetRoutes()
        {
            var generics = MedicineService.GetRoutes("", "");
            Assert.IsTrue(generics.Count > 0, "No generics found");
            //Assert.IsTrue(generics.Contains("rect"), "A specific route is not found.");
        }

        [TestMethod]
        public void _can_GetShapes()
        {
            var generics = MedicineService.GetShapes("", "");
            Assert.IsTrue(generics.Count > 0, "No generics found");
            //Assert.IsTrue(generics.Contains("tabl"), "A specific shape is not found");
        }
    }
}
