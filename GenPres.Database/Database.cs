using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Database
{
    public static class DatabaseConnection
    {
        public enum DatabaseName
        {
            FORMULARIUM2010,
            GENPRES
        }

        public static void SetSettingsPath(string path)
        {
            Settings.SettingsManager.Instance.SetSettingsPath(path);
        }

        public static string GetConnectionString(DatabaseName database)
        {
            string connection = string.Empty;

            switch (database)
            {
                case DatabaseName.GENPRES:
                    connection = Settings.SettingsManager.Instance.ReadSecureSetting("GenPresDBConnectionString");
                    break;    
                default:
                    throw new Exception("Database not found");
            }
            return connection;
        }
    }
}
