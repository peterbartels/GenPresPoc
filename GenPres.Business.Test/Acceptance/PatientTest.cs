using System.Collections.ObjectModel;
using GenPres.Business.Data.Client.Patient;
using GenPres.Business.Data.DataAccess.Repositories;
using GenPres.Business.Domain.PatientDomain;
using GenPres.Business.Service;
using GenPres.Business.ServiceProvider;
using GenPres.DataAccess.Repositories;
using GenPres.xTest.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace GenPres.Business.Test.Acceptance
{
    [TestClass]
    public class PatientTest : BaseGenPresTest
    {
     
        private IPdsmRepository _initializePatientTest()
        {
            var repository = Isolate.Fake.Instance<PdmsRepository>(Members.CallOriginal);
            DalServiceProvider.Instance.RegisterInstanceOfType<IPdsmRepository>(repository);
            return repository;
        }

        [TestMethod]
        public void _can_GetPatientsByLogicalId()
        {
            _initializePatientTest();
            ReadOnlyCollection<PatientTreeDto> patients = PatientService.GetPatientsByLogicalUnit(1);
            Assert.IsTrue(patients.Count > 0);
        }

        [TestMethod]
        public void _can_GetPatientsByPid()
        {
            _initializePatientTest();
            ReadOnlyCollection<PatientTreeDto> patients = PatientService.GetPatientsByLogicalUnit(1);
            Assert.IsTrue(patients.Count > 0);
        }

        [TestMethod]
        public void _can_SaveAPatient()
        {
            var patient = Patient.GetPatientByPid("8697898");
            patient.Save();
        }
    }
}
