using GenPres.Business.Calculation;
using GenPres.Business.Domain.Prescriptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Calculation
{
    [TestClass]
    public class QuantityConcentrationCalculationTests
    {
        [TestMethod]
        public void ThatDrugConcentrationCanBeCalculatedWithDoseQtyAndAdminQty()
        {
            var prescription = Prescription.NewPrescription();
            
            prescription.Quantity.SetValue(2, "zetp", "", "", "");
            prescription.FirstDose.Quantity.SetValue(200, "mg", "", "", "");
            prescription.FirstSubstance.DrugConcentration.SetValue(0, "mg", "", "zetp", "");

            var calculator = new PrescriptionCalculator(prescription);
            calculator.Calculate();
            Assert.AreEqual(100, prescription.FirstSubstance.DrugConcentration.Value);
        }

        [TestMethod]
        public void ThatDoseQtyCanBeCalculatedWithDrugConcentrationAndAdminQty()
        {
            var prescription = Prescription.NewPrescription();

            prescription.Quantity.SetValue(2, "zetp", "", "", "");
            prescription.FirstDose.Quantity.SetValue(0, "mg", "", "", "");
            prescription.FirstSubstance.DrugConcentration.SetValue(100, "mg", "", "zetp", "");

            var calculator = new PrescriptionCalculator(prescription);
            calculator.Calculate();
            Assert.AreEqual(200, prescription.FirstDose.Quantity.Value);
        }


        [TestMethod]
        public void ThatAdminQtyCanBeCalculatedWithDrugConcentrationAndDoseQty()
        {
            var prescription = Prescription.NewPrescription();

            prescription.Quantity.SetValue(0, "zetp", "", "", "");
            prescription.FirstDose.Quantity.SetValue(200, "mg", "", "", "");
            prescription.FirstSubstance.DrugConcentration.SetValue(100, "mg", "", "zetp", "");

            var calculator = new PrescriptionCalculator(prescription);
            calculator.Calculate();
            Assert.AreEqual(2, prescription.Quantity.Value);
        }
    }
}
