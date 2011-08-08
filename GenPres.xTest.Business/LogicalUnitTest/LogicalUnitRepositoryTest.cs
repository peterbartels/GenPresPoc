using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Patients;
using GenPres.Data.Repositories;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace GenPres.xTest.Business.LogicalUnitTest
{
    [TestClass]
    public class LogicalUnitRepositoryTest : BaseGenPresTest
    {
        private static void InitializeLogicalUnitTest()
        {
            var repository = Isolate.Fake.Instance<LogicalUnitRepository>(Members.CallOriginal);
            StructureMap.ObjectFactory.Configure(x => x.For<ILogicalUnitRepository>().Use(repository));
            return;
        }

        [Isolated]
        [TestMethod]
        public void LogicalUnitGetLogicalUnitsCallsRepositoryGetLogicalUnits()
        {
            InitializeLogicalUnitTest();
            var logicalUnitRepository = StructureMap.ObjectFactory.GetInstance<ILogicalUnitRepository>();
            LogicalUnit.GetLogicalUnits();
            Isolate.Verify.WasCalledWithAnyArguments(() => logicalUnitRepository.GetLogicalUnits());
        }
    }
}
