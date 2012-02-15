using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Patients;
using GenPres.Data.DAO.Mapper.Patients;
using GenPres.Data.Managers;
using GenPres.Data.Repositories;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using TypeMock.ArrangeActAssert;

namespace GenPres.xTest.Business.LogicalUnitTest
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
