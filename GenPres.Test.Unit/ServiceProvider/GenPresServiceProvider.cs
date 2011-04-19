using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.Business;
using TypeMock;
using TypeMock.ArrangeActAssert;
using Enterprise.Service;

namespace GenPres.Test.Unit.ServiceProvider
{
    [TestClass]
    public class GenPresServiceProviderTest
    {
        #region Constructor
        public GenPresServiceProviderTest()
        {
        }
        #endregion

        #region Context
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
        public void _does_Resolve_Instance()
        {
            IServiceProvider genPresService = GenPresServiceProvider.Create();
            var fakeInstance = Isolate.Fake.Instance<ITestChild>();
            genPresService.RegisterInstanceOfType<ITestParent>(fakeInstance);

            IServiceProvider genPresService2 = GenPresServiceProvider.Create();
            ITestParent test = genPresService2.Resolve<ITestParent>();
            Assert.IsTrue(test != null);
        }
    }

    #region Test Interfaces
    public interface ITestParent
    {

    }
    public interface ITestChild : ITestParent
    {

    }
    #endregion

}
