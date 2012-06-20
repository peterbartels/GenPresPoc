using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Business.Domain.Prescriptions.Scenarios;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenPres.xTest.Business.PrescriptionScenarios
{
    [TestClass]
    public class AVisibilityScenarioShould
    {
        private Prescription _prescription;
        private VisibilityScenario _visibilityScenario;

        [TestInitialize]
        public void SetupIsolatedVisibilityScenario()
        {
            _prescription = Isolate.Fake.Instance<Prescription>();
            _visibilityScenario = new VisibilityScenario();
        }

    }
}
