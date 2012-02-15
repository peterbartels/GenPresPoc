using System;
using System.Globalization;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GenPres.Assembler;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Business.GenPresInitialize
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
