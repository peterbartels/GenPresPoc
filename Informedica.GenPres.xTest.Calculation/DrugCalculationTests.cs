using Informedica.GenPres.Business.Calculation;
using Informedica.GenPres.Business.Domain.Prescriptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenPres.xTest.Calculation
{
    [TestClass]
    public class DrugCalculationTests
    {
        [TestMethod]
        public void ThatSubstanceQuantityCanBeCalculatedWithDrugQuantityAndSubstanceDrugConcentration()
        {
            var prescription = Prescription.NewPrescription();

            prescription.FirstSubstance.Quantity.Unit = "mg";
            prescription.FirstSubstance.DrugConcentration.Unit = "mg";
            prescription.FirstSubstance.DrugConcentration.Total = "ml";
            prescription.FirstSubstance.DrugConcentration.Value = 2;
            prescription.Drug.Quantity.Unit = "ml";
            prescription.Drug.Quantity.Value = 200;

            var calculator = new PrescriptionCalculator(prescription);
            calculator.Calculate();
            Assert.AreEqual(400, prescription.FirstSubstance.Quantity.Value);
        }

        [TestMethod]
        public void ThatSubstanceDrugConcentrationCanBeCalculatedWithDrugQuantityAndSubstanceQuantity()
        {
            var prescription = Prescription.NewPrescription();

            prescription.FirstSubstance.Quantity.Unit = "mg";
            prescription.FirstSubstance.Quantity.Value = 400;
            prescription.FirstSubstance.DrugConcentration.Unit = "mg";
            prescription.FirstSubstance.DrugConcentration.Total = "ml";
            prescription.Drug.Quantity.Unit = "ml";
            prescription.Drug.Quantity.Value = 200;

            var calculator = new PrescriptionCalculator(prescription);
            calculator.Calculate();
            Assert.AreEqual(2, prescription.FirstSubstance.DrugConcentration.Value);
        }

        [TestMethod]
        public void ThatDrugQuantityCanBeCalculatedWithSubstanceQuantityAndSubstanceDrugConcentration()
        {
            var prescription = Prescription.NewPrescription();

            prescription.FirstSubstance.Quantity.Unit = "mg";
            prescription.FirstSubstance.Quantity.Value = 400;
            prescription.FirstSubstance.DrugConcentration.Unit = "mg";
            prescription.FirstSubstance.DrugConcentration.Total = "ml";
            prescription.FirstSubstance.DrugConcentration.Value = 2;
            prescription.Drug.Quantity.Unit = "ml";

            var calculator = new PrescriptionCalculator(prescription);
            calculator.Calculate();
            Assert.AreEqual(200, prescription.Drug.Quantity.Value);
        }
    }
}
