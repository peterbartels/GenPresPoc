using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Business.Domain.Prescriptions.Scenarios;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenPres.xTest.Business.PrescriptionScenarios
{
    [TestClass]
    public class AnOptionScenarioShould
    {
        private Prescription _prescription;
        private OptionScenario _optionsScenario;

        [TestInitialize]
        public void SetupIsolatedOptionScenario()
        {
            _prescription = Isolate.Fake.Instance<Prescription>();
            _optionsScenario = new OptionScenario();
        }

        [Isolated]
        [TestMethod]
        public void CallPrescriptionSolutionWhenMethodAppliesToIsCalled()
        {
            _optionsScenario.AppliesTo(_prescription);
            Isolate.Verify.WasCalledWithAnyArguments(() => _prescription.Solution);
        }

        [Isolated]
        [TestMethod]
        public  void ReturnTrueWhenAppliesToPrescriptionWithSolutionIsTrue()
        {
            Isolate.WhenCalled(() => _prescription.Solution).WillReturn(true);
            _optionsScenario.Solution = true;
            Assert.IsTrue(_optionsScenario.AppliesTo(_prescription));
        }

        [Isolated]
        [TestMethod]
        public void CallPrescriptionContinuousWhenMethodAppliesToIsCalled()
        {
            _optionsScenario.AppliesTo(_prescription);
            Isolate.Verify.WasCalledWithAnyArguments(() => _prescription.Continuous);
        }

        [Isolated]
        [TestMethod]
        public void ReturnTrueWhenAppliesToPrescriptionWithContinuousIsTrue()
        {
            Isolate.WhenCalled(() => _prescription.Continuous).WillReturn(true);
            _optionsScenario.Continuous = true;
            Assert.IsTrue(_optionsScenario.AppliesTo(_prescription));
        }

        [Isolated]
        [TestMethod]
        public void CallPrescriptionInfusionWhenMethodAppliesToIsCalled()
        {
            _optionsScenario.AppliesTo(_prescription);
            Isolate.Verify.WasCalledWithAnyArguments(() => _prescription.Infusion);
        }

        [Isolated]
        [TestMethod]
        public void ReturnTrueWhenAppliesToPrescriptionWithInfusionIsTrue()
        {
            Isolate.WhenCalled(() => _prescription.Infusion).WillReturn(true);
            _optionsScenario.Infusion = true;
            Assert.IsTrue(_optionsScenario.AppliesTo(_prescription));
        }

        [Isolated]
        [TestMethod]
        public void CallPrescriptionOnRequestWhenMethodAppliesToIsCalled()
        {
            _optionsScenario.AppliesTo(_prescription);
            Isolate.Verify.WasCalledWithAnyArguments(() => _prescription.OnRequest);
        }

        [Isolated]
        [TestMethod]
        public void ReturnTrueWhenAppliesToPrescriptionWithOnRequestIsTrue()
        {
            Isolate.WhenCalled(() => _prescription.OnRequest).WillReturn(true);
            _optionsScenario.OnRequest = true;
            Assert.IsTrue(_optionsScenario.AppliesTo(_prescription));
        }

        [Isolated]
        [TestMethod]
        public void ReturnTrueWhenAppliesToPrescriptionWithAllOptionsSetToTrue()
        {
            Isolate.WhenCalled(() => _prescription.Solution).WillReturn(true);
            Isolate.WhenCalled(() => _prescription.Continuous).WillReturn(true);
            Isolate.WhenCalled(() => _prescription.Infusion).WillReturn(true);
            Isolate.WhenCalled(() => _prescription.OnRequest).WillReturn(true);

            _optionsScenario.Solution = true;
            _optionsScenario.Continuous = true;
            _optionsScenario.Infusion = true;
            _optionsScenario.OnRequest = true;
            Assert.IsTrue(_optionsScenario.AppliesTo(_prescription));
        }
    }
}
