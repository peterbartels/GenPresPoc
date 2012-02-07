using GenPres.Business.Calculation;
using GenPres.Business.Domain.Prescriptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Calculation
{
    [TestClass]
    public class RateConcentrationCalculationTests
    {
        [TestMethod]
        public void ThatDrugConcentrationCanBeCalculatedWithDoseRateAndAdminRate()
        {
            var prescription = Prescription.NewPrescription();

            prescription.Rate.SetValue(2, "zetp", "uur", "", "");
            prescription.FirstDose.Rate.SetValue(200, "mg", "uur", "", "");
            prescription.FirstSubstance.DrugConcentration.SetValue(0, "mg", "", "zetp", "");

            var calculator = new PrescriptionCalculator(prescription);
            calculator.Calculate();
            Assert.AreEqual(100, prescription.FirstSubstance.DrugConcentration.Value);
        }

        [TestMethod]
        public void ThatDoseRateCanBeCalculatedWithDrugConcentrationAndAdminRate()
        {
            var prescription = Prescription.NewPrescription();

            prescription.Rate.SetValue(2, "zetp", "uur", "", "");
            prescription.FirstDose.Rate.SetValue(0, "mg", "uur", "", "");
            prescription.FirstSubstance.DrugConcentration.SetValue(100, "mg", "", "zetp", "");

            var calculator = new PrescriptionCalculator(prescription);
            calculator.Calculate();
            Assert.AreEqual(200, prescription.FirstDose.Rate.Value);
        }


        [TestMethod]
        public void ThatAdminRateCanBeCalculatedWithDrugConcentrationAndDoseRate()
        {
            var prescription = Prescription.NewPrescription();

            prescription.Rate.SetValue(0, "zetp", "uur", "", "");
            prescription.FirstDose.Rate.SetValue(200, "mg", "uur", "", "");
            prescription.FirstSubstance.DrugConcentration.SetValue(100, "mg", "", "zetp", "");

            var calculator = new PrescriptionCalculator(prescription);
            calculator.Calculate();
            Assert.AreEqual(2, prescription.Rate.Value);
        }
    }
}
