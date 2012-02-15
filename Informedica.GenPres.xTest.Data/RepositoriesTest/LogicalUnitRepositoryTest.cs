using Informedica.GenPres.Business.Data.IRepositories;
using Informedica.GenPres.Business.Domain.Patients;
using Informedica.GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenPres.xTest.Data.RepositoriesTest
{
    [TestClass]
    public class LogicalUnitRepositoryTests : BaseGenPresTest
    {
        [Isolated]
        [TestMethod]
        public void ThatPdmsRepositoryCanGetPatientByPid()
        {
            var repos = IsolateObjectMethod<ILogicalUnitRepository>("GetLogicalUnitsFromDatabase", new[] { LogicalUnit.NewLogicalUnit() });
            Assert.IsTrue(repos.GetLogicalUnits().Length > 0);
        }

        
    }
}
