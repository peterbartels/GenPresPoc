using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Business.Domain.Prescriptions.Scenarios;
using Informedica.GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenPres.xTest.Business.PrescriptionScenarios 
{
    [TestClass]
    public class ScenariosTest : BaseGenPresTest
    {
        [TestMethod]
        public void ThatAdminVolumeIsDefaultFalse()
        {
            var prescription = Prescription.NewPrescription();
            Assert.IsFalse(prescription.AdminVolume, "AdminVolume should be default false");
        }

        [TestMethod]
        public void ThatDoseVolumeIsDefaultFalse()
        {
            var prescription = Prescription.NewPrescription();
            Assert.IsFalse(prescription.DoseVolume, "DoseVolume should be default false");
        }

        [TestMethod]
        public void ThatVolumeScenarioWithAllSettingsFalseAppliesToPrescriptionWithVolumeSettingsFalse()
        {
            var prescription = Isolate.Fake.Instance<Prescription>();
            Isolate.WhenCalled(() => prescription.AdminVolume).WillReturn(true);
            var volumeScenario = new VolumeScenario();
            Assert.IsTrue(volumeScenario.AppliesTo(prescription));
        }
        [TestMethod]
        public void ThatAnOptionsScenarioCanEqualAPrescription()
        {
            var prescription = Prescription.NewPrescription();
            var optionsScenario = new OptionScenario();
            Assert.IsTrue(optionsScenario.AppliesTo(prescription));
        }
    }
}

