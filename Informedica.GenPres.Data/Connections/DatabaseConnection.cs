using System;
using System.Collections.Generic;
using Informedica.GenPres.Business.Domain.Databases;
using Settings;

namespace Informedica.GenPres.Data.Connections
{
    public class DatabaseConnection: IDatabaseConnection
    {
        public enum DatabaseName 
        {
            GenPres,
            GenPresTest
        }

        public static string GetConnectionString(DatabaseName database, String name) 
        {
            var instance = new DatabaseConnection();
            
            return instance.GetConnectionString(Enum.GetName(typeof(DatabaseName), database), name);
        }

        public static string GetComputerName()
        {
            return Environment.MachineName;
        }

        #region Implementation of IDatabaseConnection

        public Boolean TestConnection(String connectionString)
        {
            try
            {
                using (System.Data.IDbConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    connection.Open();
                    connection.Close();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }                            
        }

        public void RegisterSetting(IDatabaseSetting databaseSetting)
        {
            SettingsManager.Instance.CreateSecureSetting(databaseSetting.Machine, 
                                                         databaseSetting.Name,
                                                         SettingsManager.GenPresConnectionString,
                                                         databaseSetting.GenPresConnectionString);
            SettingsManager.Instance.CreateSecureSetting(databaseSetting.Machine,
                                                         databaseSetting.Name,
                                                         SettingsManager.PdmsConnectionString,
                                                         databaseSetting.PdmsConnectionString);
            SettingsManager.Instance.CreateSecureSetting(databaseSetting.Machine,
                                                         databaseSetting.Name,
                                                         SettingsManager.GenFormWebservice,
                                                         databaseSetting.GenFormWebservice);
        }

        public string GetConnectionString(String database, String name)
        {
            return SettingsManager.Instance.ReadSecureSetting(database, name);
        }

        public void SetSettingsPath(string path)
        {
            SettingsManager.Instance.Initialize(path);
        }

        public IEnumerable<string> GetDatabases()
        {
            return SettingsManager.Instance.GetNames();
        }

        public static string GetLocalConnectionString(DatabaseName databaseName)
        {
            return GetConnectionString(databaseName, "GenPresConnectionString");
        }
        #endregion

    }
}
