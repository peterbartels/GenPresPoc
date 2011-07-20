using System;
using System.Collections.Generic;
using GenPres.Business.Domain.DatabaseDomain;

namespace GenPres.Business.Service
{
    public interface IDatabaseServices

    {
        Boolean TestDatabaseConnection(IDatabaseSetting databaseSetting);
        Boolean RegisterDatabaseSetting(IDatabaseSetting databaseSetting);
        void MapSettingsPath(String path);
        IEnumerable<String> GetDatabases();
    }
}