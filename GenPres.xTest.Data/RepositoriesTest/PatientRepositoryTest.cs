using System;
using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Patients;
using GenPres.Business.Domain.Prescriptions;
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
    }
}
