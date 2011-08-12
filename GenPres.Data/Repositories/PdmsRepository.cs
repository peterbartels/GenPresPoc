using System;
using System.Collections.Generic;
using System.Linq;
using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Patients;
using GenPres.Data.DAO.Mapper.Patient;
using GenPres.Data.Managers;

namespace GenPres.Data.Repositories
{
    public class PdmsRepository : IPdsmRepository
    {
        private PdmsMapper _pdmsMapper = new PdmsMapper();

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
        public List<Patient> GetPatientsByLogicalUnitId(int logicalUnitId)
        {
            var sqlQuery = GetSql();
            sqlQuery += "pat.DischargeDate IS NULL AND pat.LOGICALUNITID='" + logicalUnitId + "' ORDER BY pat.LastName;";

            var sqlResult = PDMSDataRetriever.ExecuteSQL(sqlQuery);

            Patient[] patients = new Patient[sqlResult.Tables[0].Rows.Count];

            for (int i = 0; i < sqlResult.Tables[0].Rows.Count; i++)
            {
                patients[i] = _pdmsMapper.MapDaoToBusinessObject(sqlResult.Tables[0].Rows[i], Patient.NewPatient());
            }

            return patients.ToList();
        }

        public Patient GetPatientByPid(string pid)
        {
            var sqlQuery = GetSql();
            sqlQuery += "pat.DischargeDate IS NULL AND pat.HospitalNumber='" + pid + "' ORDER BY pat.LastName;";

            var sqlResult = PDMSDataRetriever.ExecuteSQL(sqlQuery);

            if(sqlResult.Tables[0].Rows.Count != 1)
            {
                throw new Exception("Trying to find a non-existing Pid:" + pid);
            }

            var patient = _pdmsMapper.MapDaoToBusinessObject(sqlResult.Tables[0].Rows[0], Patient.NewPatient());
            return patient;
        }

        public Patient GetPatientsByPatientId(string patientId)
        {
            string sqlQuery = "";
            sqlQuery += "SELECT pat.*, b.BedName, lu.Name as LogicalUnitName, ";
            sqlQuery += "(SELECT TOP 1 Signals.Value FROM Signals WHERE (Signals.ParameterID = 9505 OR Signals.ParameterID = 2896) AND Signals.PatientID = pat.PatientID AND Signals.Error = 0 ORDER BY Signals.Time DESC) as Length, ";
            sqlQuery += "(SELECT TOP 1 Signals.Value FROM Signals WHERE (Signals.ParameterID = 8458 OR Signals.ParameterID = 8460) AND Signals.PatientID = pat.PatientID AND Signals.Error = 0 ORDER BY Signals.Time DESC) as Weight ";
            sqlQuery += "FROM Patients pat ";
            sqlQuery += "LEFT JOIN LogicalUnits lu ON lu.LogicalUnitID = pat.LOGICALUNITID ";
            sqlQuery += "LEFT JOIN Beds b ON b.BedID = pat.BedID ";
            sqlQuery += "WHERE ";
            sqlQuery += "pat.DischargeDate IS NULL AND pat.HospitalNumber='" + patientId + "' ORDER BY pat.LastName;";

            var sqlResult = PDMSDataRetriever.ExecuteSQL(sqlQuery);

            return _pdmsMapper.MapDaoToBusinessObject(sqlResult.Tables[0].Rows[0], Patient.NewPatient());
        }
    }
}
