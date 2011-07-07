using GenPres.Business.Data.DataAccess.Repository;
using GenPres.Business.Domain.Patient;
using GenPres.Business.ServiceProvider;
using GenPres.xTest.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.DataAccess.Repository;
using TypeMock.ArrangeActAssert;

namespace GenPres.Business.Test.LogicalUnitTest
{
    [TestClass]
    public class LogicalUnitRepositoryTest : BaseGenPresTest
    {
        private ILogicalUnitRepository _initializeLogicalUnitTest()
        {
            var repository = Isolate.Fake.Instance<LogicalUnitRepository>(Members.CallOriginal);
            DalServiceProvider.Instance.RegisterInstanceOfType<ILogicalUnitRepository>(repository);
            return repository;
        }

        [Isolated]
        [TestMethod]
        public void LogicalUnitGetLogicalUnits_calls_RepositoryGetLogicalUnits()
        {
            _initializeLogicalUnitTest();
            var logicalUnitRepository = DalServiceProvider.Instance.Resolve<ILogicalUnitRepository>();
            LogicalUnit.GetLogicalUnits();
            Isolate.Verify.WasCalledWithAnyArguments(() => logicalUnitRepository.GetLogicalUnits());
        }
    }
}
