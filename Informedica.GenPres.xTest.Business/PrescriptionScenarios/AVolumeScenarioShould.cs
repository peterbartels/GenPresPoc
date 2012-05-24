using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Business.Domain.Prescriptions.Scenarios;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenPres.xTest.Business.PrescriptionScenarios
{
    [TestClass]
    public class AVolumeScenarioShould
    {
        private Prescription _prescription;
        private VolumeScenario _volumeScenario;

        [TestInitialize]
        public void SetupIsolatedVolumeScenario()
        {
            _prescription = Isolate.Fake.Instance<Prescription>();
            _volumeScenario = new VolumeScenario();
        }

        [Isolated]
        [TestMethod]
        public void CallPrescriptionAdminVolumeWhenCalledAppliesTo()
        {
            _volumeScenario.AppliesTo(_prescription);
            Isolate.Verify.WasCalledWithAnyArguments(() => _prescription.AdminVolume);
        }

        [Isolated]
        [TestMethod]
        public void CallPrescriptionDoseVolumeWhenCalledAppliesTo()
        {
            _volumeScenario.AppliesTo(_prescription);
            Isolate.Verify.WasCalledWithAnyArguments(() => _prescription.DoseVolume);
        }

        [Isolated]
        [TestMethod]
        public void ReturnFalseWhenPrescriptionDoseVolumeIsSetAndScenarioHasDoseVolumeFalse()
        {
            _volumeScenario.DoseVolume = false;
            Isolate.WhenCalled(() => _prescription.DoseVolume).WillReturn(true);

            Assert.IsFalse(_volumeScenario.AppliesTo(_prescription));
        }

        [Isolated]
        [TestMethod]
        public void ReturnFalseWhenPrescriptionAdminVolumeIsSetAndScenarioHasAdminVolumeFalse()
        {
            _volumeScenario.AdminVolume = false;
            Isolate.WhenCalled(() => _prescription.AdminVolume).WillReturn(true);

            Assert.IsFalse(_volumeScenario.AppliesTo(_prescription));
        }

        [Isolated]
        [TestMethod]
        public void ReturnTrueWhenPrescriptionVolumesAreSetAndScenarioVolumesAreTrue()
        {
            _volumeScenario.AdminVolume = true;
            _volumeScenario.DoseVolume = true;

            Isolate.WhenCalled(() => _prescription.AdminVolume).WillReturn(true);
            Isolate.WhenCalled(() => _prescription.DoseVolume).WillReturn(true);
            Assert.IsTrue(_volumeScenario.AppliesTo(_prescription));
        }
    }
}
