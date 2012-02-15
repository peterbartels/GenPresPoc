using System;
using System.Collections.Generic;

namespace GenPres.Business.Domain.Databases
{
    public interface IDatabaseConnection
    {
        Boolean TestConnection(String connectionString);
        void RegisterSetting(IDatabaseSetting databaseSetting);
        String GetConnectionString(String database, String name);
        void SetSettingsPath(string path);
        IEnumerable<String> GetDatabases();
    }
}