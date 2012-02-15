using Informedica.GenPres.Business.Calculation;
using Informedica.GenPres.Business.Domain.Prescriptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenPres.xTest.Calculation
{
    [TestClass]
    public class AdminQuantityAdminRateCalculationTest
    {
        [TestMethod]
        public void ThatAdminQuantityCanBeCalculatedWithAdminRateAndDuration()
        {
            var prescription = Prescription.NewPrescription();

            prescription.Quantity.SetValue(0, "zetp", "", "", "");
            prescription.Duration.SetValue(2, "", "uur", "", "");
            prescription.Rate.SetValue(4, "zetp", "uur", "", "");

            var calculator = new PrescriptionCalculator(prescription);
            calculator.Calculate();
            Assert.AreEqual(8, prescription.Quantity.Value);
        }

        [TestMethod]
        public void ThatAdminRateCanBeCalculatedWithAdminQuantityAndDuration()
        {
            var prescription = Prescription.NewPrescription();

            prescription.Quantity.SetValue(8, "zetp", "", "", "");
            prescription.Duration.SetValue(2, "", "uur", "", "");
            prescription.Rate.SetValue(0, "zetp", "uur", "", "");

            var calculator = new PrescriptionCalculator(prescription);
            calculator.Calculate();
            Assert.AreEqual(4, prescription.Rate.Value);
        }

        [TestMethod]
        public void ThatDurationCanBeCalculatedWithAdminQuantityAndAdminRate()
        {
            var prescription = Prescription.NewPrescription();

            prescription.Quantity.SetValue(8, "zetp", "", "", "");
            prescription.Duration.SetValue(0, "", "uur", "", "");
            prescription.Rate.SetValue(4, "zetp", "uur", "", "");

            var calculator = new PrescriptionCalculator(prescription);
            calculator.Calculate();
            Assert.AreEqual(2, prescription.Duration.Value);
        }
    }
}
