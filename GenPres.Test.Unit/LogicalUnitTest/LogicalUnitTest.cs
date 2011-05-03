using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.DataAccess;
using System.Data.SqlClient;
using System.Data;
using GenPres.Business.Data.DataAccess.Mapper;
using GenPres.Business.Domain;

namespace GenPres.Test.Unit.LogicalUnitTest
{
    [TestClass]
    public class LogicalUnitTest
    {
        public LogicalUnitTest()
        {
            Settings.SettingsManager.Instance.Initialize();
        }

        private TestContext testContextInstance;
        
        #region TestContext
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        #endregion

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

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
            DataRow logicalUnitDao = dt.NewRow();
            
            logicalUnitDao["LogicalUnitID"] = 1;
            logicalUnitDao["Name"] = "Test";
            LogicalUnitMapper logicalUnitMapper = new LogicalUnitMapper();
            
            var logicalUnit = LogicalUnit.NewLogicalUnit();

            logicalUnitMapper.MapDaoToBusinessObject(logicalUnitDao, logicalUnit);
            Assert.AreEqual(int.Parse(logicalUnitDao["LogicalUnitID"].ToString()), logicalUnit.Id);
            Assert.AreEqual(logicalUnitDao["Name"], logicalUnit.Name);
        }
    }
}
