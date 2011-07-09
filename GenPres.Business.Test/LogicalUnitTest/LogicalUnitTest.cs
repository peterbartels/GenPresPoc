using GenPres.Business.Domain.Patient;
using GenPres.DataAccess.DataMapper.Mapper.Patient;
using GenPres.xTest.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.DataAccess;
using System.Data;

namespace GenPres.Business.Test.LogicalUnitTest
{
    [TestClass]
    public class LogicalUnitTest : BaseGenPresTest
    {
        [TestMethod]
        public void PDMSDataRetriever_can_fetch_logicalUnits()
        {
            var ds = PDMSDataRetriever.ExecuteSQL("SELECT * FROM LogicalUnits WHERE LogicalUnitID IN(1,50,57)");
            Assert.IsTrue(ds.Tables[0].Rows.Count > 0);
        }

        [TestMethod]
        public void LogicalUnitMapper_in_DataAccess_can_map_logicalUnitDao_to_LogicalUnitBo()
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
