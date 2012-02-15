using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Informedica.GenPres.Business.Data.IRepositories;
using Informedica.GenPres.Business.Domain.Patients;
using Informedica.GenPres.Business.Exceptions;
using Informedica.GenPres.Data.DTO.Patients;
using Informedica.GenPres.Service;
using Informedica.GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenPres.xTest.Service
{
    [TestClass]
    public class PatientTest : BaseGenPresTest
    {

        [Isolated]
        [TestMethod]
        public void GetPatientByPatientIdShouldCallPatientRepository()
        {
            var repos = IsolateObject<IPatientRepository>();
            Isolate.WhenCalled(() => repos.GetPatientByPatientId("1")).ReturnRecursiveFake();
            var prescriptions = PatientService.GetPatientByPid("1");
            try
            {
                Isolate.Verify.WasCalledWithAnyArguments(() => repos.GetPatientByPatientId(""));
            }
            catch (Exception e)
            {
                CheckVerifiyException(e, "Patient Repository method GetPatientByPatientId was not called by Patient Service method GetPatientByPid");
            }
        }

        [Isolated]
        [TestMethod]
        public void InsertFromPdmsShouldCallPatientRepositoryPatientExists()
        {
            var repos = IsolateObject<IPatientRepository>();
            var reposPdms = IsolateObject<IPdsmPatientRepository>();
            Isolate.WhenCalled(() => repos.PatientExists("1")).WillReturn(true);
            Isolate.WhenCalled(() => reposPdms.GetPatientByPid("1")).ReturnRecursiveFake();
            Isolate.WhenCalled(() => repos.Save(null)).IgnoreCall();
            var prescriptions = PatientService.InsertFromPdms("1");
            try
            {
                Isolate.Verify.WasCalledWithAnyArguments(() => repos.PatientExists(""));
            }
            catch (Exception e)
            {
                CheckVerifiyException(e, "Patient Repository method GetPatientByPid was not called by Patient Service method GetPatientByPid");
            }
        }

        [Isolated]
        [TestMethod]
        public void InsertFromPdmsShouldCallPdmsGetPatientByPid()
        {
            var repos = IsolateObject<IPatientRepository>();
            var reposPdms = IsolateObject<IPdsmPatientRepository>();
            
            Isolate.WhenCalled(() => repos.PatientExists("1")).WillReturn(true);
            Isolate.WhenCalled(() => repos.GetPatientByPatientId("1")).ReturnRecursiveFake();
            Isolate.WhenCalled(() => reposPdms.GetPatientByPid("1")).ReturnRecursiveFake();
            Isolate.WhenCalled(() => repos.Save(null)).IgnoreCall();

            var prescriptions = PatientService.InsertFromPdms("1");
            try
            {
                Isolate.Verify.WasCalledWithAnyArguments(() => reposPdms.GetPatientByPid(""));
            }
            catch (Exception e)
            {
                CheckVerifiyException(e, "Patient Repository method GetPatientByPid was not called by Patient Service method InsertFromPdms");
            }
        }

        [Isolated]
        [TestMethod]
        public void InsertFromPdmsShouldCallGetPatientByPid()
        {
            var repos = IsolateObject<IPatientRepository>();
            var reposPdms = IsolateObject<IPdsmPatientRepository>();

            Isolate.WhenCalled(() => repos.PatientExists("1")).WillReturn(true);
            Isolate.WhenCalled(() => repos.GetPatientByPatientId("1")).ReturnRecursiveFake();
            Isolate.WhenCalled(() => reposPdms.GetPatientByPid("1")).ReturnRecursiveFake();
            Isolate.WhenCalled(() => repos.Save(null)).IgnoreCall();

            var prescriptions = PatientService.InsertFromPdms("1");
            try
            {
                Isolate.Verify.WasCalledWithAnyArguments(() => repos.GetPatientByPatientId(""));
            }
            catch (Exception e)
            {
                CheckVerifiyException(e, "Patient Repository method GetPatientByPatientId was not called by Patient Service method InsertFromPdms");
            }
        }

        [Isolated]
        [TestMethod]
        public void InsertFromPdmsShouldCallSavePatient()
        {
            var repos = IsolateObject<IPatientRepository>();
            var reposPdms = IsolateObject<IPdsmPatientRepository>();

            Isolate.WhenCalled(() => repos.PatientExists("1")).WillReturn(true);
            Isolate.WhenCalled(() => repos.GetPatientByPatientId("1")).ReturnRecursiveFake();
            Isolate.WhenCalled(() => reposPdms.GetPatientByPid("1")).ReturnRecursiveFake();
            Isolate.WhenCalled(() => repos.Save(null)).IgnoreCall();

            var prescriptions = PatientService.InsertFromPdms("1");
            try
            {
                Isolate.Verify.WasCalledWithAnyArguments(() => repos.Save(null));
            }
            catch (Exception e)
            {
                CheckVerifiyException(e, "Patient Repository method Save was not called by Patient Service method InsertFromPdms");
            }
        }

        [Isolated]
        [TestMethod]
        public void GetPatientByPatientIdShouldThrowInvalidIdExceptionWhenIdIsInvalid()
        {
            try
            {
                PatientService.GetPatientByPid("");
                Assert.Fail("GetPatientByPatientId does not throw InvalidIdException.");
            }
            catch (InvalidIdException e)
            {

            }
        }

        [Isolated]
        [TestMethod]
        public void GetPatientByLogicalUnitShouldCallPdmsRepository()
        {
            var repos = IsolateObject<IPdsmPatientRepository>();
            Isolate.WhenCalled(() => repos.GetPatientsByLogicalUnitId(1)).WillReturn(new ReadOnlyCollection<Patient>(new List<Patient>()));
            var patients = PatientService.GetPatientsByLogicalUnit(1);
            try
            {
                Isolate.Verify.WasCalledWithAnyArguments(() => repos.GetPatientsByLogicalUnitId(1));
            }
            catch (Exception e)
            {
                CheckVerifiyException(e, "PdmsRepository method GetPatientsByLogicalUnit was not called by PatientService method GetPatientsByLogicalUnit");
            }
        }

        [TestMethod]
        public void ThatPatientServiceCanGetLogicalUnits()
        {
            var logicalUnits = PatientService.GetLogicalUnits();
            Assert.IsTrue(logicalUnits.Length > 0);
        }


        [TestMethod]
        public void ThatPatientServiceCanGetPatientsByLogicalUnit()
        {
            ReadOnlyCollection<PatientTreeDto> patients = PatientService.GetPatientsByLogicalUnit(1);
            Assert.IsTrue(patients.Count > 0);
        }

        [TestMethod]
        public void ThatPatientServiceCanGetAPatientById()
        {
            PatientService.SavePatient("1234567");
            var patient = PatientService.GetPatientByPid("1234567");
            Assert.IsTrue(patient.Id != Guid.Empty);
        }
        
        [TestMethod]
        public void ThatPatientServiceCanSavePatient()
        {
            var pid = "1234567";
            var patientDto = PatientService.SavePatient(pid);
            Assert.IsTrue(patientDto.Weight.value == 12 && patientDto.Height.value == 120 && patientDto.Weight.unit == "kg" && patientDto.Height.unit == "cm");
        }
    }
}
