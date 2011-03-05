using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GenPres.Business
{
    public class DataRetriever
    {
        private string ConnectionString = Settings.SettingsManager.Instance.ReadSecureSetting("PatientDBConnectionString");

        private DataSet ExecuteSQL(string sqlQuery)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }

        public List<LogicalUnit> GetUnits()
        {
            string sqlQuery = "";
            sqlQuery += "SELECT * FROM LogicalUnits WHERE LogicalUnitID IN(1,50,57)";
            
            DataSet ds = this.ExecuteSQL(sqlQuery);
            PatientFactory pf = new PatientFactory();
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    foreach (DataRow row in ds.Tables[0].Rows)
                        pf.AddUnitByDataRow(row);

            return pf.logicalUnits;
        }

        public List<Patient> GetPatientsByLogicaUnit(string logicalUnit)
        {   
            string sqlQuery = "";
            sqlQuery += "SELECT pat.*, b.BedName, lu.Name as LogicalUnitName, ";
            sqlQuery += "(SELECT TOP 1 Signals.Value FROM Signals WHERE (Signals.ParameterID = 9505 OR Signals.ParameterID = 2896) AND Signals.PatientID = pat.PatientID AND Signals.Error = 0 ORDER BY Signals.Time DESC) as Length, ";
            sqlQuery += "(SELECT TOP 1 Signals.Value FROM Signals WHERE (Signals.ParameterID = 8458 OR Signals.ParameterID = 8460) AND Signals.PatientID = pat.PatientID AND Signals.Error = 0 ORDER BY Signals.Time DESC) as Weight ";

            sqlQuery += "FROM Patients pat ";
            sqlQuery += "LEFT JOIN LogicalUnits lu ON lu.LogicalUnitID = pat.LOGICALUNITID ";
            sqlQuery += "LEFT JOIN Beds b ON b.BedID = pat.BedID ";
            sqlQuery += "WHERE ";
            sqlQuery += "pat.DischargeDate IS NULL AND pat.LOGICALUNITID='" + logicalUnit + "' ORDER BY pat.LastName;";

            DataSet ds = this.ExecuteSQL(sqlQuery);
            PatientFactory pf = new PatientFactory();
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        pf.AddPatientByDataRow(row);
                    }

            return pf.patients;
        }
    }
}
