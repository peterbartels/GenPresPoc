using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Collections.Generic;
using Informedica.GenPres.Business.Domain.Patients;
using Informedica.GenPres.Data.DAO.Mapper.Patients;
using Informedica.GenPres.Data.DTO.Patients;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenPres.xTest.Data.DaoMappingsTest
{
    [TestClass]
    public class PatientDaoTest
    {
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

            patientDao["AddmissionDate"] = (DateTime?)DateTime.Parse("20-3-2010 13:48:00", new CultureInfo("nl-NL"));
            patientDao["LogicalUnitName"] = "Test";
            patientDao["BedName"] = "E100";

            var pdmsMapper = new PdmsPatientMapper();

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
