using System.Globalization;
using System.Threading;
using Informedica.GenPres.Assembler;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenPres.xTest.Business.GenPresInitialize
{
    [TestClass]
    public class GenPresInitializeTest
    {
        [TestMethod]
        public void AfterGenPresInitializeCultureInfoShouldBeUS()
        {
            GenPresApplication.Initialize();
            Assert.IsTrue(Thread.CurrentThread.CurrentCulture.Name == CultureInfo.CreateSpecificCulture("en-US").Name);
        }
    }
}
