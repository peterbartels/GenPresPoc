using Informedica.GenPres.Business.Calculation;
using Informedica.GenPres.Business.Domain.Prescriptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenPres.xTest.Calculation
{
    [TestClass]
    public class FreqDosageCalculationTests
    {
        [TestMethod]
        public void ThatDoseTotalCanBeCalculatedWithFrequencyAndDoseQuantity()
        {
            var prescription = Prescription.NewPrescription();
            
            prescription.Frequency.Time = "dag";
            prescription.Frequency.Value = 2;
            prescription.FirstDose.Quantity.Unit = "mg";
            prescription.FirstDose.Quantity.Value = 2;
            prescription.FirstDose.Total.Unit = "mg";
            prescription.FirstDose.Total.Time = "dag";

            var calculator = new PrescriptionCalculator(prescription);
            calculator.Calculate();
            Assert.AreEqual(4, prescription.FirstDose.Total.Value);
        }

        [TestMethod]
        public void ThatDoseQuantityCanBeCalculatedWithFrequencyAndDoseTotal()
        {
            var prescription = Prescription.NewPrescription();

            prescription.Frequency.Time = "dag";
            prescription.Frequency.Value = 2;
            prescription.FirstDose.Total.Unit = "mg";
            prescription.FirstDose.Total.Value = 4;
            prescription.FirstDose.Total.Unit = "mg";
            prescription.FirstDose.Total.Time = "dag";

            var calculator = new PrescriptionCalculator(prescription);
            calculator.Calculate();
            Assert.AreEqual(2, prescription.FirstDose.Quantity.Value);
        }

        [TestMethod]
        public void ThatFrequencyCanBeCalculatedWithDoseQuantityAndDoseTotal()
        {
            var prescription = Prescription.NewPrescription();

            prescription.FirstDose.Quantity.Unit = "mg";
            prescription.FirstDose.Quantity.Value = 4;

            prescription.FirstDose.Total.Unit = "mg";
            prescription.FirstDose.Total.Value = 8;
            prescription.FirstDose.Total.Time = "dag";

            var calculator = new PrescriptionCalculator(prescription);
            calculator.Calculate();
            Assert.AreEqual(2, prescription.Frequency.Value);
        }


    }
}
