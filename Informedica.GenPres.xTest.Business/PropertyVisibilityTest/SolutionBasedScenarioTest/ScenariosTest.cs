using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenPres.xTest.Business.PropertyVisibilityTest.SolutionBasedScenarioTest 
{
    [TestClass]
    public class ScenariosTest : BaseGenPresTest
    {
        [TestMethod]
        public void ThatAdminVolumeIsDefaultFalse()
        {
            var prescription = Prescription.NewPrescription();
            Assert.IsFalse(prescription.AdminVolume, "DoseVolume should be default false");
        }

        [TestMethod]
        public void ThatDoseVolumeIsDefaultFalse()
        {
            var prescription = Prescription.NewPrescription();
            Assert.IsFalse(prescription.DoseVolume, "DoseVolume should be default false");
        }
    }
}

