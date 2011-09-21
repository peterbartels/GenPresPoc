using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Calculation.Calculator
{
    [TestClass]
    public class SimpleCalculationTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            decimal d1 = 0.0000000000000000000000000002m;
            decimal d2 = 0.0000000000000000000000000001m;
            decimal d3 = 10m*d2;
            Assert.IsTrue(d3 > 0);
        }
    }
}