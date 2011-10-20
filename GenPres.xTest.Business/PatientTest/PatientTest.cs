using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Collections.Generic;
using System.Globalization;
using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Patients;
using GenPres.Data.DAO.Mapper.Patient;
using GenPres.Data.DTO.Patients;
using GenPres.Data.Managers;
using GenPres.Data.Repositories;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace GenPres.xTest.Business.PatientTest
{
    [TestClass]
    public class PatientTest : BaseGenPresTest
    {
        private IPdsmRepository InitializePatientRepositoryTest()
        {
            var repository = Isolate.Fake.Instance<PdmsRepository>(Members.CallOriginal);
            StructureMap.ObjectFactory.Configure(x => x.For<IPdsmRepository>().Use(repository));
            return repository;
        }

        [TestMethod]
        public void PdmsDataRetrieverCanFetchPatients()
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
        public void PatientMapperInDataAccessCanMapPatientDaoToPatientBo()
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

            patientDao["Length"] = 1.2;
            patientDao["Weight"] = 30000;
            
            patientDao["AddmissionDate"] = (DateTime?) DateTime.Parse("20-3-2010 13:48:00", new CultureInfo("nl-NL"));
            patientDao["LogicalUnitName"] = "Test";
            patientDao["BedName"] = "E100";
            
            PdmsMapper pdmsMapper = new PdmsMapper();

            var patient = Patient.NewPatient();

            pdmsMapper.MapDaoToBusinessObject(patientDao, patient);
            
            Assert.AreEqual(int.Parse(patientDao["LOGICALUNITID"].ToString()), patient.LogicalUnitId);
            Assert.AreEqual(patientDao["LastName"], patient.LastName);
            Assert.AreEqual(patientDao["FirstName"], patient.FirstName);
            Assert.AreEqual(patientDao["HospitalNumber"], patient.Pid);

            Assert.AreEqual(patientDao["Length"], patient.Height.BaseValue);
            Assert.AreEqual(patientDao["Weight"], patient.Weight.BaseValue);
            Assert.AreEqual(patientDao["AddmissionDate"], patient.RegisterDate);
            Assert.AreEqual(patientDao["LogicalUnitName"], patient.Unit);
            Assert.AreEqual(patientDao["BedName"], patient.Bed);
            
        }

        [TestMethod]
        public void PatientTreeAssemblerCanAssemblePatientTreeDto()
        {
            
            var assembler = new PatientTreeAssembler();
            var patient1 = Patient.NewPatient();
            patient1.FirstName = "Peter";
            patient1.LastName = "Bartels";
            patient1.Pid = "1234567";
            patient1.Id = Guid.Empty;
            patient1.LogicalUnitId = 2;

            patient1.Height.Value = 120;
            patient1.Weight.Value = 75;
            
            patient1.RegisterDate = DateTime.Parse("20-3-2010 13:48:00", new CultureInfo("nl-NL"));

            patient1.Unit = "unit1";
            patient1.Bed = "2";

            var collection = new List<Patient>();
            collection.Add(patient1);
            collection.Add(patient1);

            ReadOnlyCollection<PatientTreeDto> dtos = PatientTreeAssembler.AssemblePatientTreeDto(collection.AsReadOnly());
            Assert.IsTrue(dtos.Count == 2);

            Assert.AreEqual(patient1.FirstName, dtos[0].FirstName);
            Assert.AreEqual(patient1.LastName, dtos[0].LastName);
            Assert.AreEqual(patient1.Pid, dtos[0].PID);
            
            Assert.AreEqual(dtos[0].RegisterDate, "20-03-2010");
            Assert.AreEqual(patient1.Unit, dtos[0].Unit);
            Assert.AreEqual(patient1.Bed, dtos[0].Bed);

        }
    }
}

