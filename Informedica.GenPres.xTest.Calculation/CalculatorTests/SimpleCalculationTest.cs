using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenPres.xTest.Calculation.CalculatorTests
{
    [TestClass]
    public class SimpleCalculationTest
    {
        [TestMethod]
        public void ThatASimpleCalculationCanBeDonewithDecimals()
        {
            decimal d2 = 0.0000000000000000000000000001m;
            decimal d3 = 10m*d2;
            Assert.IsTrue(d3 > 0);
        }
    }
}