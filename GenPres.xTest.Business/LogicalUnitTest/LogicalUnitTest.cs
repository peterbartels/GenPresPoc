using GenPres.Business.Domain.Patients;
using GenPres.Data.DAO.Mapper.Patient;
using GenPres.Data.Managers;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace GenPres.xTest.Business.LogicalUnitTest
{
    [TestClass]
    public class LogicalUnitTest : BaseGenPresTest
    {
        [TestMethod]
        public void PdmsDataRetrieverCanFetchLogicalUnits()
        {
            var ds = PDMSDataRetriever.ExecuteSQL("SELECT * FROM LogicalUnits WHERE LogicalUnitID IN(1,50,57)");
            Assert.IsTrue(ds.Tables[0].Rows.Count > 0);
        }

        [TestMethod]
        public void LogicalUnitMapperInDataAccessCanMapLogicalUnitDaoToLogicalUnitBo()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("LogicalUnitID");
            dt.Columns.Add("Name");
            var logicalUnitDao = dt.NewRow();
            
            logicalUnitDao["LogicalUnitID"] = 1;
            logicalUnitDao["Name"] = "Test";
            var logicalUnitMapper = new LogicalUnitMapper();
            
            var logicalUnit = LogicalUnit.NewLogicalUnit();

            logicalUnitMapper.MapFromBoToDao(logicalUnitDao, logicalUnit);
            Assert.AreEqual(int.Parse(logicalUnitDao["LogicalUnitID"].ToString()), logicalUnit.Id);
            Assert.AreEqual(logicalUnitDao["Name"], logicalUnit.Name);
        }
    }
}
