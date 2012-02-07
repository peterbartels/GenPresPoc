using GenPres.Business.Calculation;
using GenPres.Business.Domain.Prescriptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Calculation
{
    [TestClass]
    public class TotalConcentrationCalculationTests
    {
        [TestMethod]
        public void ThatDrugConcentrationCanBeCalculatedWithDoseTotalAndAdminTotal()
        {
            var prescription = Prescription.NewPrescription();

            prescription.Total.SetValue(2, "zetp", "dag", "", "");
            prescription.FirstDose.Total.SetValue(200, "mg", "dag", "", "");
            prescription.FirstSubstance.DrugConcentration.SetValue(0, "mg", "", "zetp", "");

            var calculator = new PrescriptionCalculator(prescription);
            calculator.Calculate();
            Assert.AreEqual(100, prescription.FirstSubstance.DrugConcentration.Value);
        }

        [TestMethod]
        public void ThatDoseTotalCanBeCalculatedWithDrugConcentrationAndAdminTotal()
        {
            var prescription = Prescription.NewPrescription();

            prescription.Total.SetValue(2, "zetp", "dag", "", "");
            prescription.FirstDose.Total.SetValue(0, "mg", "dag", "", "");
            prescription.FirstSubstance.DrugConcentration.SetValue(100, "mg", "", "zetp", "");

            var calculator = new PrescriptionCalculator(prescription);
            calculator.Calculate();
            Assert.AreEqual(200, prescription.FirstDose.Total.Value);
        }


        [TestMethod]
        public void ThatAdminTotalCanBeCalculatedWithDrugConcentrationAndDoseTotal()
        {
            var prescription = Prescription.NewPrescription();

            prescription.Total.SetValue(0, "zetp", "dag", "", "");
            prescription.FirstDose.Total.SetValue(200, "mg", "dag", "", "");
            prescription.FirstSubstance.DrugConcentration.SetValue(100, "mg", "", "zetp", "");

            var calculator = new PrescriptionCalculator(prescription);
            calculator.Calculate();
            Assert.AreEqual(2, prescription.Total.Value);
        }
    }
}
