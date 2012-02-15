using Informedica.GenPres.Business.Data.IRepositories;
using Informedica.GenPres.Business.Domain.Patients;
using Informedica.GenPres.Data.Repositories;
using Informedica.GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenPres.xTest.Business.LogicalUnitTest
{
    [TestClass]
    public class LogicalUnitTest : BaseGenPresTest
    {
        [TestMethod]
        public void PdmsDataRetrieverCanFetchLogicalUnits()
        {
            //var ds = PDMSDataRetriever.ExecuteSQL("SELECT * FROM LogicalUnits WHERE LogicalUnitID IN(1,50,57)");
            //Assert.IsTrue(ds.Tables[0].Rows.Count > 0);
        }

        private static void InitializeLogicalUnitTest()
        {
            var repository = Isolate.Fake.Instance<LogicalUnitRepository>(Members.CallOriginal);
            StructureMap.ObjectFactory.Configure(x => x.For<ILogicalUnitRepository>().Use(repository));
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
