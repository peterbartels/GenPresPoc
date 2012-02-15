using Informedica.GenPres.Business.Calculation;
using Informedica.GenPres.Business.Domain.Prescriptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenPres.xTest.Calculation
{
    [TestClass]
    public class DoseQuantityDoseRateCalculationTests
    {
        [TestMethod]
        public void ThatDoseQuantityCanBeCalculatedWithDoseRateAndDuration()
        {
            var prescription = Prescription.NewPrescription();

            prescription.FirstDose.Quantity.SetValue(0, "mg", "", "", "");
            prescription.Duration.SetValue(2, "", "uur", "", "");
            prescription.FirstDose.Rate.SetValue(4, "mg", "uur", "", "");

            var calculator = new PrescriptionCalculator(prescription);
            calculator.Calculate();
            Assert.AreEqual(8, prescription.FirstDose.Quantity.Value);
        }

        [TestMethod]
        public void ThatDoseRateCanBeCalculatedWithDoseQuantityAndDuration()
        {
            var prescription = Prescription.NewPrescription();

            prescription.FirstDose.Quantity.SetValue(8, "mg", "", "", "");
            prescription.Duration.SetValue(2, "", "uur", "", "");
            prescription.FirstDose.Rate.SetValue(0, "mg", "uur", "", "");

            var calculator = new PrescriptionCalculator(prescription);
            calculator.Calculate();
            Assert.AreEqual(4, prescription.FirstDose.Rate.Value);
        }

        [TestMethod]
        public void ThatDurationCanBeCalculatedWithDoseQuantityAndDoseRate()
        {
            var prescription = Prescription.NewPrescription();

            prescription.FirstDose.Quantity.SetValue(8, "mg", "", "", "");
            prescription.Duration.SetValue(0, "", "uur", "", "");
            prescription.FirstDose.Rate.SetValue(4, "mg", "uur", "", "");

            var calculator = new PrescriptionCalculator(prescription);
            calculator.Calculate();
            Assert.AreEqual(2, prescription.Duration.Value);
        }
    }
}
