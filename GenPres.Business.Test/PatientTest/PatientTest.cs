using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Collections.Generic;
using GenPres.Business.Data.DataAccess.Repositories;
using GenPres.Business.Domain.Patients;
using GenPres.DataAccess.DAO.Mapper.Patient;
using GenPres.DataAccess.DTO.Patients;
using GenPres.DataAccess.Managers;
using GenPres.DataAccess.Repositories;
using GenTest=GenPres.xTest.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace GenPres.Business.Test.PatientTest
{
    [TestClass]
    public class PatientTest : GenTest.BaseGenPresTest
    {
        private IPdsmRepository _initializePatientRepositoryTest()
        {
            var repository = Isolate.Fake.Instance<PdmsRepository>(Members.CallOriginal);
            StructureMap.ObjectFactory.Configure(x => x.For<IPdsmRepository>().Use(repository));
            return repository;
        }

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
            dt.Columns.Add("LOGICALUNITID", typeof(int));

            dt.Columns.Add("Length", typeof(decimal));
            dt.Columns.Add("Weight", typeof(decimal));
            dt.Columns.Add("AddmissionDate", typeof(DateTime));
            dt.Columns.Add("LogicalUnitName");
            dt.Columns.Add("BedName");
            dt.Columns.Add("GenderID");

            DataRow patientDao = dt.NewRow();

            patientDao["PatientID"] = 1;
            patientDao["FirstName"] = "FTest";
            patientDao["LastName"] = "LTest";
            patientDao["HospitalNumber"] = "1234567";
            patientDao["LOGICALUNITID"] = 1;

            patientDao["Length"] = 120;
            patientDao["Weight"] = 30;
            
            patientDao["AddmissionDate"] = (DateTime?) DateTime.Parse("20-3-2010 13:48:00");
            patientDao["LogicalUnitName"] = "Test";
            patientDao["BedName"] = "E100";
            
            PdmsMapper pdmsMapper = new PdmsMapper();

            var patient = Patient.NewPatient();

            pdmsMapper.MapDaoToBusinessObject(patientDao, patient);
            Assert.AreEqual(int.Parse(patientDao["PatientID"].ToString()), patient.Id);
            Assert.AreEqual(int.Parse(patientDao["LOGICALUNITID"].ToString()), patient.LogicalUnitId);
            Assert.AreEqual(patientDao["LastName"], patient.LastName);
            Assert.AreEqual(patientDao["FirstName"], patient.FirstName);
            Assert.AreEqual(patientDao["HospitalNumber"], patient.PID);

            Assert.AreEqual(patientDao["Length"], patient.Length);
            Assert.AreEqual(patientDao["Weight"], patient.Weight);
            Assert.AreEqual(patientDao["AddmissionDate"], patient.RegisterDate);
            Assert.AreEqual(patientDao["LogicalUnitName"], patient.Unit);
            Assert.AreEqual(patientDao["BedName"], patient.Bed);
            
        }

        [TestMethod]
        public void PatientTreeAssembler_can_Assemble_PatientTreeDto()
        {
            var assembler = new PatientTreeAssembler();
            var patient1 = Patient.NewPatient();
            patient1.FirstName = "Peter";
            patient1.LastName = "Bartels";
            patient1.PID = "1234567";
            patient1.Id = 1;
            patient1.LogicalUnitId = 2;

            patient1.Length = 120;
            patient1.Weight = 75;
            patient1.RegisterDate = DateTime.Parse("20-3-2010 13:48:00");
            patient1.Unit = "unit1";
            patient1.Bed = "2";


            var collection = new List<IPatient>();
            collection.Add(patient1);
            collection.Add(patient1);

            ReadOnlyCollection<PatientTreeDto> dtos = PatientTreeAssembler.AssemblePatientTreeDto(collection.AsReadOnly());
            Assert.IsTrue(dtos.Count == 2);

            Assert.AreEqual(patient1.FirstName, dtos[0].FirstName);
            Assert.AreEqual(patient1.LastName, dtos[0].LastName);
            Assert.AreEqual(patient1.PID, dtos[0].PID);

            Assert.AreEqual(patient1.Weight, dtos[0].Weight);
            Assert.AreEqual(patient1.Length, dtos[0].Length);
            Assert.AreEqual(dtos[0].RegisterDate, "20-03-2010");
            Assert.AreEqual(patient1.Unit, dtos[0].Unit);
            Assert.AreEqual(patient1.Bed, dtos[0].Bed);

        }
    }
}

