﻿using System;
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
            var patient = Patient.GetPatientByPid("8697898");
            patient.Save();
            Assert.IsTrue(patient.Id != Guid.Empty);
        }
        
        [TestMethod]
        public void CanCreateAPatient()
        {
            var uid = System.Guid.NewGuid();
            var patient = Patient.GetPatientByPid(uid.ToString());
            patient.Save();
            Assert.IsTrue(patient.Id != Guid.Empty);
        }

        [TestMethod]
        public void CanSelectPatient()
        {
            var pid = "0004702";
            bool created = PatientService.SelectPatient(pid);
            Assert.IsTrue(created);
        }
    }
}
