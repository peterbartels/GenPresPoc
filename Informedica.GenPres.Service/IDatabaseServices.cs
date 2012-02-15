using System;
using System.Collections.Generic;
using Informedica.GenPres.Business.Domain.Databases;

namespace Informedica.GenPres.Service
{
    public interface IDatabaseServices

    {
        Boolean TestDatabaseConnection(IDatabaseSetting databaseSetting);
        Boolean RegisterDatabaseSetting(IDatabaseSetting databaseSetting);
        void MapSettingsPath(String path);
        IEnumerable<String> GetDatabases();
    }
}