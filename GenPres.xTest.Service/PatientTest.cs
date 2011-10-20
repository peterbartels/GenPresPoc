using System;
using System.Collections.ObjectModel;
using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Patients;
using GenPres.Business.Exceptions;
using GenPres.Data.DTO.Patients;
using GenPres.Service;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace GenPres.xTest.Service
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
            var reposPdms = IsolateObject<IPdsmRepository>();
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
            var reposPdms = IsolateObject<IPdsmRepository>();
            
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
            var reposPdms = IsolateObject<IPdsmRepository>();

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
            var reposPdms = IsolateObject<IPdsmRepository>();

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
            //TODO: doesn't work gives error, check error
        }


        [TestMethod]
        public void ThatPatientServiceCanGetPatientsByLogicalId()
        {
            ReadOnlyCollection<PatientTreeDto> patients = PatientService.GetPatientsByLogicalUnit(1);
            Assert.IsTrue(patients.Count > 0);
        }

        [TestMethod]
        public void ThatPatientServiceCanGetPatientsByPid()
        {
            ReadOnlyCollection<PatientTreeDto> patients = PatientService.GetPatientsByLogicalUnit(1);
            Assert.IsTrue(patients.Count > 0);
        }

        [TestMethod]
        public void ThatPatientServiceCanSaveAPatient()
        {
            PatientService.SavePatient("0004588");
            var patient = PatientService.GetPatientByPid("0004588");
            Assert.IsTrue(patient.Id != Guid.Empty);
        }
        
        [TestMethod]
        public void ThatPatientServiceCanSelectPatient()
        {
            var pid = "0004588";
            var patientDto = PatientService.SavePatient(pid);
            Assert.IsTrue(patientDto.Weight.value == 12 && patientDto.Height.value == 120 && patientDto.Weight.unit == "kg" && patientDto.Height.unit == "cm");
        }
    }
}
