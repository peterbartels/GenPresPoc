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
            var connectionString = Settings.SettingsManager.Instance.ReadSecureSetting(
                Settings.SettingsManager.DatabaseName, 
                Settings.SettingsManager.PdmsConnectionString
            );

            var conn = new SqlConnection(connectionString);
            var adapter = new SqlDataAdapter(sqlQuery, conn);
            var ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }
    }
}
