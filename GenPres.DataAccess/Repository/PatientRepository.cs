using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GenPres.Business.Data.DataAccess.Mapper;
using GenPres.Business.Data.DataAccess.Mapper.Patient;
using GenPres.Business.Data.DataAccess.Repository;
using GenPres.Business.Domain;
using GenPres.Business.Domain.Patient;
using DB = GenPres.Database;
using GenPres.Business.Domain;


namespace GenPres.DataAccess.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private PatientMapper _patientMapper = new PatientMapper();

        public List<IPatient> GetPatientsByLogicalUnitId(int logicalUnitId)
        {
            string sqlQuery = "";
            sqlQuery += "SELECT pat.*, b.BedName, lu.Name as LogicalUnitName, ";
            sqlQuery += "(SELECT TOP 1 Signals.Value FROM Signals WHERE (Signals.ParameterID = 9505 OR Signals.ParameterID = 2896) AND Signals.PatientID = pat.PatientID AND Signals.Error = 0 ORDER BY Signals.Time DESC) as Length, ";
            sqlQuery += "(SELECT TOP 1 Signals.Value FROM Signals WHERE (Signals.ParameterID = 8458 OR Signals.ParameterID = 8460) AND Signals.PatientID = pat.PatientID AND Signals.Error = 0 ORDER BY Signals.Time DESC) as Weight ";

            sqlQuery += "FROM Patients pat ";
            sqlQuery += "LEFT JOIN LogicalUnits lu ON lu.LogicalUnitID = pat.LOGICALUNITID ";
            sqlQuery += "LEFT JOIN Beds b ON b.BedID = pat.BedID ";
            sqlQuery += "WHERE ";
            sqlQuery += "pat.DischargeDate IS NULL AND pat.LOGICALUNITID='" + logicalUnitId + "' ORDER BY pat.LastName;";

            var sqlResult = PDMSDataRetriever.ExecuteSQL(sqlQuery);

            IPatient[] patients = new IPatient[sqlResult.Tables[0].Rows.Count];

            for (int i = 0; i < sqlResult.Tables[0].Rows.Count; i++)
            {
                patients[i] = _patientMapper.MapDaoToBusinessObject(sqlResult.Tables[0].Rows[i], Patient.NewPatient());
            }

            return patients.ToList();
        }
    }
}
