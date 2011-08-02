using System.Collections.ObjectModel;
using GenPres.Business.Data.Client.Patient;
using GenPres.Business.Domain.PatientDomain;
using GenPres.Business.Service;
using GenTest=GenPres.xTest.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.Business.Test.Acceptance
{
    [TestClass]
    public class PatientTest : GenTest.BaseGenPresTest
    {
        [TestMethod]
        public void _can_GetPatientsByLogicalId()
        {
            ReadOnlyCollection<PatientTreeDto> patients = PatientService.GetPatientsByLogicalUnit(1);
            Assert.IsTrue(patients.Count > 0);
        }

        [TestMethod]
        public void _can_GetPatientsByPid()
        {
            ReadOnlyCollection<PatientTreeDto> patients = PatientService.GetPatientsByLogicalUnit(1);
            Assert.IsTrue(patients.Count > 0);
        }

        [TestMethod]
        public void _can_SaveAPatient()
        {
            var patient = Patient.GetPatientByPid("8697898");
            patient.Save();
            Assert.IsTrue(patient.Id > 0);
        }
        
        [TestMethod]
        public void _can_CreateAPatient()
        {
            var uid = System.Guid.NewGuid();
            var patient = Patient.GetPatientByPid(uid.ToString());
            patient.Save();
            Assert.IsTrue(patient.Id > 0);
        }

    }
}
