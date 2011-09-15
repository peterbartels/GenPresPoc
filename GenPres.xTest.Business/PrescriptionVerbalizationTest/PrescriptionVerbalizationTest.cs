using GenPres.Business.Calculation;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Business.Verbalization;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Business.PrescriptionVerbalizationTest
{
    [TestClass]
    public class PrescriptionVerbalizationTest : BaseGenPresTest
    {
        [TestMethod]
        public void ParacetamolRectNoOptions()
        {
            var prescription = CreateParacetamolRect(Prescription.NewPrescription());
            prescription.Frequency.Value = 3;
            prescription.PatientWeight.Value = 10.0m;
            prescription.Doses[0].Quantity.Value = 240;

            prescription.Doses[0].Total.Adjust = "kg";
            prescription.Doses[0].Total.Time = "week";
            prescription.Quantity.Value = 3;

            PrescriptionCalculator.Calculate(prescription);

            var verbalization = PrescriptionVerbalization.Verbalize(prescription);
            Assert.IsTrue(verbalization == "paracetamol 240 mg zetpil rect 3 keer per dag 1 zetpil = 240 mg, 21 zetpil/week = 72 mg/ kg/dag");
        }
    }
}
