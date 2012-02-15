using System;
using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Patients;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Data.Repositories;
using GenPres.Service;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using TypeMock.ArrangeActAssert;

namespace GenPres.xTest.Data.RepositoriesTest
{
    [TestClass]
    public class PatientRepositoryTests : BaseGenPresTest
    {
        [Isolated]
        [TestMethod]
        public void ThatPatientCanBeGetByPatientId()
        {
            var repos = IsolateObjectMethod<IPatientRepository>("GetPatientByPatientId", Patient.NewPatient());
            Assert.IsNotNull(repos.GetPatientByPatientId("1234567"));
        }

        [Isolated]
        [TestMethod]
        public void ThatPatientCanCheckIfPatientExists()
        {
            var repos = IsolateObjectMethod<IPatientRepository>("PatientExists", true);
            Assert.IsNotNull(repos.PatientExists("1234567"));
        }

        [Isolated]
        [TestMethod]
        public void ThatPatientCanNotBeBeGetByEmptyPatientId()
        {
            var repos = ObjectFactory.GetInstance<IPatientRepository>();
            Assert.IsNull(repos.GetPatientByPatientId(""));
        }

        private static void InitializePatientRepositoryTest()
        {
            var repository = Isolate.Fake.Instance<PdmsRepository>(Members.CallOriginal);
            StructureMap.ObjectFactory.Configure(x => x.For<IPdsmPatientRepository>().Use(repository));
            return;
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
            patientRep.SaveOrUpdate(pat);
            Assert.IsTrue(pat.Id != Guid.Empty);
            Assert.IsTrue(pat.Pid != "");
        }

        private void PatientRepositoryCanGetAPatient()
        {
            var patientRep = new PatientRepository();
            var pat = PatientService.GetPatientByPid("1234567");
            Assert.IsTrue(pat.Id != Guid.Empty);
            Assert.IsTrue(pat.Pid == "1234567");
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
