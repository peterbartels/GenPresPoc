using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Calculation.Calculator
{
    [TestClass]
    public class PropertyIncrementTest : BaseGenPresTest
    {
        [TestMethod]
        public void CanRectifyPropertyToClosestIncrement()
        {
            var prescription = PrescriptionTestFunctions.GetTestPrescription();
            prescription.Frequency.Value = 1.5m;
            var pc = PrescriptionTestFunctions.SetCombinations(prescription);

            pc.ExecuteCalculation();
            pc.FinishCalculation();
            Assert.IsTrue(prescription.Frequency.Value == 2);
        }
    }
}
