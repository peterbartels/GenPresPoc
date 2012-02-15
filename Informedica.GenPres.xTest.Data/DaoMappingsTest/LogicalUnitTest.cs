using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Patients;
using GenPres.Data.DAO.Mapper.Patients;
using GenPres.Data.Repositories;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using TypeMock.ArrangeActAssert;

namespace GenPres.xTest.Data.DaoMappingsTest
{
    [TestClass]
    public class LogicalUnitTest : BaseGenPresTest
    {
        private static void InitializeLogicalUnitTest()
        {
            var repository = Isolate.Fake.Instance<LogicalUnitRepository>(Members.CallOriginal);
            StructureMap.ObjectFactory.Configure(x => x.For<ILogicalUnitRepository>().Use(repository));
        }

        [TestMethod]
        public void PdmsLogicalUnitMapperInDataAccessCanMapLogicalUnitDaoToLogicalUnitBo()
        {
            var dt = new DataTable();
            dt.Columns.Add("LogicalUnitID");
            dt.Columns.Add("Name");
            var logicalUnitDao = dt.NewRow();
            
            logicalUnitDao["LogicalUnitID"] = 1;
            logicalUnitDao["Name"] = "Test";
            var logicalUnitMapper = new PdmsLogicalUnitMapper();
            
            var logicalUnit = LogicalUnit.NewLogicalUnit();

            logicalUnitMapper.MapFromBoToDao(logicalUnitDao, logicalUnit);
            Assert.AreEqual(int.Parse(logicalUnitDao["LogicalUnitID"].ToString()), logicalUnit.Id);
            Assert.AreEqual(logicalUnitDao["Name"], logicalUnit.Name);
        }

        [TestMethod]
        public void XmlLogicalUnitMapperInDataAccessCanMapLogicalUnitDaoToLogicalUnitBo()
        {
            int id = 0;
            string name = "test";
            var logicalUnitMapper = new XmlLogicalUnitMapper();

            var logicalUnit = LogicalUnit.NewLogicalUnit();

            logicalUnitMapper.MapFromBoToDao(id, name, logicalUnit);
            Assert.AreEqual(id, logicalUnit.Id);
            Assert.AreEqual(name, logicalUnit.Name);
        }
    }
}
