using System;
using GenPres.Business.Service;
using GenPres.Business.WebService;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

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
        public void MedicineServiceGetGenericsShouldCallGenFormWebServices()
        {
            var genformService = IsolateObject<IGenFormWebServices>();
            Isolate.WhenCalled(() => genformService.GetGenerics("", "")).WillReturn(new string[]{});
            var generics = MedicineService.GetGenerics("", "");
            try
            {
                Isolate.Verify.WasCalledWithAnyArguments(() => genformService.GetGenerics("", ""));
            }
            catch (Exception e)
            {
                CheckVerifiyException(e, "GenFormWebServices method GetGenerics was not called by Medicine Service method GetGenerics");
            }
        }

        [TestMethod]
        public void MedicineServiceGetRoutesShouldCallGenFormWebServices()
        {
            var genformService = IsolateObject<IGenFormWebServices>();
            Isolate.WhenCalled(() => genformService.GetRoutes("", "")).WillReturn(new string[] { });
            var routes = MedicineService.GetRoutes("", "");
            try
            {
                Isolate.Verify.WasCalledWithAnyArguments(() => genformService.GetRoutes("", ""));
            }
            catch (Exception e)
            {
                CheckVerifiyException(e, "GenFormWebServices method GetRoutes was not called by Medicine Service method GetRoutes");
            }
        }

        [TestMethod]
        public void MedicineServiceGetShapesShouldCallGenFormWebServices()
        {
            var genformService = IsolateObject<IGenFormWebServices>();
            Isolate.WhenCalled(() => genformService.GetShapes("", "")).WillReturn(new string[] { });
            var shapes = MedicineService.GetShapes("", "");
            try
            {
                Isolate.Verify.WasCalledWithAnyArguments(() => genformService.GetShapes("", ""));
            }
            catch (Exception e)
            {
                CheckVerifiyException(e, "GenFormWebServices method GetShapes was not called by Medicine Service method GetShapes");
            }
        }

        [TestMethod]
        public void ThatMedicineServiceCanGetGenerics()
        {
            var generics = MedicineService.GetGenerics("", "");
            Assert.IsTrue(generics.Count > 0, "No generics found");
        }

        [TestMethod]
        public void ThatMedicineServiceCanGetRoutes()
        {
            var generics = MedicineService.GetRoutes("", "");
            Assert.IsTrue(generics.Count > 0, "No generics found");
        }

        [TestMethod]
        public void ThatMedicineServiceCanGetShapes()
        {
            var generics = MedicineService.GetShapes("", "");
            Assert.IsTrue(generics.Count > 0, "No generics found");
        }
    }
}
