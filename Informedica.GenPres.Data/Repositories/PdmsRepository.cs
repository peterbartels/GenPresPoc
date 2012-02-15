using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using Informedica.GenPres.Business.Data.IRepositories;
using Informedica.GenPres.Business.Domain.Patients;
using Informedica.GenPres.Data.DAO.Mapper.Patients;
using Informedica.GenPres.Data.Managers;

namespace Informedica.GenPres.Data.Repositories
{
    public class PdmsRepository : IPdsmPatientRepository
    {
        private PdmsPatientMapper _pdmsMapper = new PdmsPatientMapper();
        private XmlPatientMapper _xmlMapper = new XmlPatientMapper();

        public static string GetSql()
        {
            string sqlQuery = "";
            sqlQuery += "SELECT pat.*, b.BedName, lu.Name as LogicalUnitName, ";
            sqlQuery += "(SELECT TOP 1 Signals.Value FROM Signals WHERE (Signals.ParameterID = 9505 OR Signals.ParameterID = 2896) AND Signals.PatientID = pat.PatientID AND Signals.Error = 0 ORDER BY Signals.Time DESC) as Length, ";
            sqlQuery += "(SELECT TOP 1 Signals.Value FROM Signals WHERE (Signals.ParameterID = 8458 OR Signals.ParameterID = 8460) AND Signals.PatientID = pat.PatientID AND Signals.Error = 0 ORDER BY Signals.Time DESC) as Weight ";

            sqlQuery += "FROM Patients pat ";
            sqlQuery += "LEFT JOIN LogicalUnits lu ON lu.LogicalUnitID = pat.LOGICALUNITID ";
            sqlQuery += "LEFT JOIN Beds b ON b.BedID = pat.BedID ";
            sqlQuery += "WHERE ";
            return sqlQuery;
        }

        public ReadOnlyCollection<Patient> GetPatientsByLogicalUnitId(int logicalUnitId)
        {
            return GetPatientsByLogicalUnitFromXml(logicalUnitId).ToList().AsReadOnly();
        }

        public Patient GetPatientByPidFromXml(string pid)
        {
            var xmlDoc = XDocument.Parse(Informedica.GenPres.Data.Properties.Resources.patients);
            
            if(pid == "")
            {
                return null;
            }

            var patientObj =
                from c in xmlDoc.Descendants("patients")
                where c.Descendants("hospitalnumber").ElementAt(0).Value == pid
                select c;
            
            var patient = _xmlMapper.MapDaoToBusinessObject(patientObj.ElementAt(0), Patient.NewPatient());
            
            return patient;
        }

        public Patient[] GetPatientsByLogicalUnitFromXml(int logicalUnitId)
        {
            var xmlDoc = XDocument.Parse(Informedica.GenPres.Data.Properties.Resources.patients);

            XElement logicalUnitElement;
            try
            {
                logicalUnitElement = xmlDoc.Descendants("logicalunit").ElementAt(logicalUnitId);
            }catch(Exception e)
            {
                return new Patient[0];
            }

            var patientsObj =
                from c in logicalUnitElement.Descendants("patients")
                select c;

            var patients = new Patient[patientsObj.Count()];

            int id = 0;
            foreach (var patientObj in patientsObj)
            {
                patients[id] = _xmlMapper.MapDaoToBusinessObject(patientObj, Patient.NewPatient());
                id++;
            }

            return patients;
        }

        public Patient[] GetPatientsByLogicalUnitFromDatabase(int logicalUnitId)
        {
            var sqlQuery = GetSql();
            sqlQuery += "pat.DischargeDate IS NULL AND pat.LOGICALUNITID='" + logicalUnitId + "' ORDER BY pat.LastName;";

            var sqlResult = PDMSDataRetriever.ExecuteSQL(sqlQuery);

            Patient[] patients = new Patient[sqlResult.Tables[0].Rows.Count];

            for (int i = 0; i < sqlResult.Tables[0].Rows.Count; i++)
            {
                patients[i] = _pdmsMapper.MapDaoToBusinessObject(sqlResult.Tables[0].Rows[i], Patient.NewPatient());
            }

            return patients;
        }

        public Patient GetPatientByByPidFromDatabase(string pid)
        {
            var sqlQuery = GetSql();
            sqlQuery += "pat.DischargeDate IS NULL AND pat.HospitalNumber='" + pid + "' ORDER BY pat.LastName;";

            var sqlResult = PDMSDataRetriever.ExecuteSQL(sqlQuery);

            Patient patient = null;

            if (sqlResult.Tables[0].Rows.Count == 1)
            {
                patient = _pdmsMapper.MapDaoToBusinessObject(sqlResult.Tables[0].Rows[0], Patient.NewPatient());
            }

            return patient;
        }


        public Patient GetPatientByPid(string pid)
        {
            return GetPatientByPidFromXml(pid);
        }
    }
}
