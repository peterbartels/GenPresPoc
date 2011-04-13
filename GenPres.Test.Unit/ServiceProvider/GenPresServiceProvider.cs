using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.Business;
using TypeMock;
using TypeMock.ArrangeActAssert;

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
            GenPresServiceProvider genPresService = GenPresServiceProvider.Create();
            var fakeInstance = Isolate.Fake.Instance<ITestChild>();
            genPresService.RegisterInstance<ITestParent>(fakeInstance);
            ITestParent test = genPresService.Resolve<ITestParent>();
            Assert.IsTrue(test is ITestChild);
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
