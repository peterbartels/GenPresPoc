using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using GenPres.Business.Data.DataAccess.Mapper;
using GenPres.Business.Domain;
using GenPres.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.Test.Unit.PatientTest
{
    [TestClass]
    public class PatientTest
    {
        public PatientTest()
        {
            Settings.SettingsManager.Instance.Initialize();
        }

        #region TestContext
        private TestContext testContextInstance;
        
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
        public void PDMSDataRetriever_can_fetch_Patients()
        {
            string sqlQuery = "";
            sqlQuery += "SELECT pat.*, b.BedName, lu.Name as LogicalUnitName, ";
            sqlQuery += "(SELECT TOP 1 Signals.Value FROM Signals WHERE (Signals.ParameterID = 9505 OR Signals.ParameterID = 2896) AND Signals.PatientID = pat.PatientID AND Signals.Error = 0 ORDER BY Signals.Time DESC) as Length, ";
            sqlQuery += "(SELECT TOP 1 Signals.Value FROM Signals WHERE (Signals.ParameterID = 8458 OR Signals.ParameterID = 8460) AND Signals.PatientID = pat.PatientID AND Signals.Error = 0 ORDER BY Signals.Time DESC) as Weight ";

            sqlQuery += "FROM Patients pat ";
            sqlQuery += "LEFT JOIN LogicalUnits lu ON lu.LogicalUnitID = pat.LOGICALUNITID ";
            sqlQuery += "LEFT JOIN Beds b ON b.BedID = pat.BedID ";
            sqlQuery += "WHERE ";
            sqlQuery += "pat.DischargeDate IS NULL AND pat.LOGICALUNITID='1' ORDER BY pat.LastName;";

            var ds = PDMSDataRetriever.ExecuteSQL(sqlQuery);
            Assert.IsTrue(ds.Tables[0].Rows.Count > 0);
        }


        [TestMethod]
        public void PatientMapper_in_DataAccess_can_map_PatientDao_to_PatientBo()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PatientID");
            dt.Columns.Add("FirstName");
            dt.Columns.Add("LastName");
            dt.Columns.Add("HospitalNumber");
            dt.Columns.Add("LOGICALUNITID");

            DataRow patientDao = dt.NewRow();

            patientDao["PatientID"] = 1;
            patientDao["FirstName"] = "FTest";
            patientDao["LastName"] = "LTest";
            patientDao["HospitalNumber"] = "1234567";
            patientDao["LOGICALUNITID"] = 1;

            PatientMapper patientMapper = new PatientMapper();

            var patient = Patient.NewPatient();

            patientMapper.MapDaoToBusinessObject(patientDao, patient);
            Assert.AreEqual(int.Parse(patientDao["PatientID"].ToString()), patient.Id);
            Assert.AreEqual(int.Parse(patientDao["LOGICALUNITID"].ToString()), patient.LogicalUnitId);
            Assert.AreEqual(patientDao["LastName"], patient.LastName);
            Assert.AreEqual(patientDao["FirstName"], patient.FirstName);
            Assert.AreEqual(patientDao["HospitalNumber"], patient.PID);
        }
    }
}
