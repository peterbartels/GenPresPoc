using GenPres.Business.Data.DataAccess.Repositories;
using GenPres.Business.Domain.Patients;
using GenPres.DataAccess.Repositories;
using GenPres.xTest.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace GenPres.Business.Test.LogicalUnitTest
{
    [TestClass]
    public class LogicalUnitRepositoryTest : BaseGenPresTest
    {
        private ILogicalUnitRepository _initializeLogicalUnitTest()
        {
            var repository = Isolate.Fake.Instance<LogicalUnitRepository>(Members.CallOriginal);
            StructureMap.ObjectFactory.Configure(x => x.For<ILogicalUnitRepository>().Use(repository));
            return repository;
        }

        [Isolated]
        [TestMethod]
        public void LogicalUnitGetLogicalUnits_calls_RepositoryGetLogicalUnits()
        {
            _initializeLogicalUnitTest();
            var logicalUnitRepository = StructureMap.ObjectFactory.GetInstance<ILogicalUnitRepository>();
            LogicalUnit.GetLogicalUnits();
            Isolate.Verify.WasCalledWithAnyArguments(() => logicalUnitRepository.GetLogicalUnits());
        }
    }
}
