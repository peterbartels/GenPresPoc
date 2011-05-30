using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using GenPres.Business.WebService;
using GenPres.xTest.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace GenPres.Test.Unit.GenForm
{
    /// <summary>
    /// Summary description for WebServicesTest
    /// </summary>
    [TestClass]
    public class WebServicesTest : BaseGenPresTest
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

        public void _can_GetRoutes_from_GenFormService()
        {
            var genFormService = Isolate.Fake.Instance<IGenFormService>();
            Isolate.WhenCalled(() => genFormService.GetGenerics()).WillReturn(new string[] {"paracetamol", "dopamine"});
            var generics = genFormService.GetGenerics();
            Assert.IsTrue(generics.Length > 0);
        }


    }
}
