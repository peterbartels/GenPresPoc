using GenPres.Business.Domain.Prescriptions;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Calculation.CalculatorTests
{
    [TestClass]
    public class PropertyIncrementTest : BaseGenPresTest
    {
        [TestMethod]
        public void CanRectifyPropertyToClosestIncrement()
        {
            var prescription = CreateParacetamolRect(Prescription.NewPrescription());
            prescription.Frequency.Value = 1.5m;
            var pc = PrescriptionTestFunctions.SetCombinations(prescription);

            pc.Execute();
            pc.Finish();
            Assert.IsTrue(prescription.Frequency.Value == 2);
        }
    }
}
