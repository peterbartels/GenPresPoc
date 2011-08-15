﻿using System;
using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Patients;
using GenPres.Data;
using GenPres.Data.Repositories;
using GenPres.Service;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;


namespace GenPres.xTest.Business.PatientTest
{
    [TestClass]
    public class PatientRepositoryTest : BaseGenPresTest
    {
        private static void InitializePatientRepositoryTest()
        {
            var repository = Isolate.Fake.Instance<PdmsRepository>(Members.CallOriginal);
            StructureMap.ObjectFactory.Configure(x => x.For<IPdsmRepository>().Use(repository));
            return;
        }

        [Isolated]
        [TestMethod]
        public void PatientServiceGetPatientsByLogicalUnitIdCallsRepository()
        {
            InitializePatientRepositoryTest();
            var patientRepository = StructureMap.ObjectFactory.GetInstance<IPdsmRepository>();
            PatientService.GetPatientsByLogicalUnit(1);
            Isolate.Verify.WasCalledWithExactArguments(() => patientRepository.GetPatientsByLogicalUnitId(1));
        }

        [TestMethod]
        public void RunTests()
        {
            PatientRepositoryPatientExistsCanInsertAPatient();
            PatientRepositoryPatientExistsCanFindAPatient();
            PatientRepositoryPatientExistsCanNotFindAPatient();
        }

        private void PatientRepositoryPatientExistsCanInsertAPatient()
        {
            var patientRep = new PatientRepository();
            var pat = Patient.NewPatient();
            pat.Pid = "1234567";
            pat.Save();
            Assert.IsTrue(pat.Id != Guid.Empty);
        }

        private void PatientRepositoryPatientExistsCanFindAPatient()
        {
            var patientRep = new PatientRepository();
            var exists = patientRep.PatientExists("1234567");
            Assert.IsTrue(exists);
            
        }

        private void PatientRepositoryPatientExistsCanNotFindAPatient()
        {
            var patientRep = new PatientRepository();
            var exists = patientRep.PatientExists("qqqqqqq");
            Assert.IsTrue(exists == false);
        }
    }
}
