using GenPres.Business.Calculation;
using GenPres.Business.Domain.Prescriptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Calculation
{
    [TestClass]
    public class FreqAdminCalculationTests
    {
           
        [TestMethod]
        public void ThatAdminTotalCanBeCalculatedWithFrequencyAndAdminQuantity()
        {
            var prescription = Prescription.NewPrescription();

            prescription.Frequency.Time = "dag";
            prescription.Frequency.Value = 2;
            prescription.Quantity.Unit = "zetp";
            prescription.Quantity.Value = 2;
            prescription.Total.Unit = "zetp";
            prescription.Total.Time = "dag";

            var calculator = new PrescriptionCalculator(prescription);
            calculator.Calculate();
            Assert.AreEqual(4, prescription.Total.Value);
        }


        [TestMethod]
        public void ThatAdminQuantityCanBeCalculatedWithFrequencyAndAdminTotal()
        {
            var prescription = Prescription.NewPrescription();

            prescription.Frequency.Time = "dag";
            prescription.Frequency.Value = 2;
            prescription.Total.Unit = "zetp";
            prescription.Total.Value = 4;
            prescription.Total.Time = "dag";
            prescription.Quantity.Unit = "zetp";
            var calculator = new PrescriptionCalculator(prescription);
            calculator.Calculate();
            Assert.AreEqual(2, prescription.Quantity.Value);
        }

        [TestMethod]
        public void ThatFrequencyCanBeCalculatedWithQuantityAndTotal()
        {
            var prescription = Prescription.NewPrescription();

            prescription.Quantity.Unit = "zetp";
            prescription.Quantity.Value = 4;

            prescription.Total.Unit = "zetp";
            prescription.Total.Value = 8;
            prescription.Total.Time = "dag";

            var calculator = new PrescriptionCalculator(prescription);
            calculator.Calculate();
            Assert.AreEqual(2, prescription.Frequency.Value);
        }

    }
}
