using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Patients;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using TypeMock.ArrangeActAssert;

namespace GenPres.xTest.Data.RepositoriesTest
{
    [TestClass]
    public class PdmsRepositoryTests : BaseGenPresTest
    {
        [Isolated]
        [TestMethod]
        public void ThatPdmsRepositoryCanGetPatientByPid()
        {
            var repos = IsolateObjectMethod<IPdsmPatientRepository>("GetPatientByByPidFromDatabase", Patient.NewPatient());
            Assert.IsNotNull(repos.GetPatientByPid("1234567"));
        }

        [Isolated]
        [TestMethod]
        public void ThatPdmsRepositoryCanGetPatientsByLogicalUnit()
        {
            var repos = IsolateObjectMethod<IPdsmPatientRepository>("GetPatientsByLogicalUnitFromDatabase", new[] { Patient.NewPatient() });
            Assert.IsTrue(repos.GetPatientsByLogicalUnitId(1).Count > 0);
        }

        [Isolated]
        [TestMethod]
        public void ThatPdmsRepositoryCanNotGetPatientsByLogicalUnitWithNonExistingId()
        {
            var repos = ObjectFactory.GetInstance<IPdsmPatientRepository>();
            Assert.IsTrue(repos.GetPatientsByLogicalUnitId(999999).Count == 0);
        }

        [Isolated]
        [TestMethod]
        public void ThatPdmsRepositoryCanNotGetPatientByPidWithEmptyId()
        {
            var repos = ObjectFactory.GetInstance<IPdsmPatientRepository>();
            Assert.IsNull(repos.GetPatientByPid(""));
        }
    }
}
