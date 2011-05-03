using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GenPres.DataAccess
{
    public class PDMSDataRetriever
    {
        public static DataSet ExecuteSQL(string sqlQuery)
        {
            string ConnectionString = Settings.SettingsManager.Instance.ReadSecureSetting("PatientDBConnectionString");
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }
    }
}
