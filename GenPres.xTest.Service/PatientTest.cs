using System;
using System.Collections.ObjectModel;
using GenPres.Business.Domain.Patients;
using GenPres.Data.DTO.Patients;
using GenPres.Service;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Service
{
    [TestClass]
    public class PatientTest : BaseGenPresTest
    {
        [TestMethod]
        public void CanGetPatientsByLogicalId()
        {
            ReadOnlyCollection<PatientTreeDto> patients = PatientService.GetPatientsByLogicalUnit(1);
            Assert.IsTrue(patients.Count > 0);
        }

        [TestMethod]
        public void CanGetPatientsByPid()
        {
            ReadOnlyCollection<PatientTreeDto> patients = PatientService.GetPatientsByLogicalUnit(1);
            Assert.IsTrue(patients.Count > 0);
        }

        [TestMethod]
        public void CanSaveAPatient()
        {
            var patient = PatientService.GetPatientByPid("8697898");
            PatientService.SavePatient("8697898");
            Assert.IsTrue(patient.Id != Guid.Empty);
        }
        
        [TestMethod]
        public void CanCreateAPatient()
        {
            var uid = System.Guid.NewGuid();
            var patient = PatientService.GetPatientByPid(uid.ToString());
            PatientService.SavePatient("8697898");
            Assert.IsTrue(patient.Id != Guid.Empty);
        }

        [TestMethod]
        public void CanSelectPatient()
        {
            var pid = "0004588";
            PatientDto patientDto = PatientService.SavePatient(pid);
            Assert.IsTrue(patientDto.Weight.value == 12 && patientDto.Height.value == 120 && patientDto.Weight.unit == "kg" && patientDto.Height.unit == "cm");
        }
    }
}
