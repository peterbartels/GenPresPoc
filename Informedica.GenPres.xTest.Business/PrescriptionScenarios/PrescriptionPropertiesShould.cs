using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Business.Domain.Prescriptions.Scenarios;
using Informedica.GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenPres.xTest.Business.PrescriptionScenarios 
{
    [TestClass]
    public class PrescriptionPropertiesShould : BaseGenPresTest
    {
        [TestMethod]
        public void Have_a_default_admin_volume_set_to_false()
        {
            var prescription = Prescription.NewPrescription();
            Assert.IsFalse(prescription.AdminVolume, "AdminVolume should be default false");
        }

        [TestMethod]
        public void Have_a_default_dose_volume_set_to_false()
        {
            var prescription = Prescription.NewPrescription();
            Assert.IsFalse(prescription.DoseVolume, "DoseVolume should be default false");
        }

        [TestMethod]
        public void Return_dose_volume_true_when_type_of_substance_unit_is_a_volume()
        {
            var prescription = Prescription.NewPrescription();
            prescription.FirstSubstance.Quantity.Unit = "ml";
            Assert.IsFalse(prescription.DoseVolume, "DoseVolume should be default false");
        }

        [TestMethod]
        public void Return_admin_volume_true_when_type_of_drug_unit_is_a_volume()
        {
            var prescription = Prescription.NewPrescription();
            prescription.Drug.Quantity.Unit = "ml";
            Assert.IsFalse(prescription.DoseVolume, "DoseVolume should be default false");
        }

    }
}

