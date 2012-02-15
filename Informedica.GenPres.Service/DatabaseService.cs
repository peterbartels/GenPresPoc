using System;
using System.Collections.Generic;
using Informedica.GenPres.Business.Domain.Databases;
using StructureMap;

namespace Informedica.GenPres.Service
{
    public class DatabaseServices : IDatabaseServices
    {
        #region Implementation of IDatabaseServices

        public bool TestDatabaseConnection(IDatabaseSetting databaseSetting)
        {
            return GetDatabaseConnection().TestConnection(databaseSetting.GenPresConnectionString);
        }

        public bool RegisterDatabaseSetting(IDatabaseSetting databaseSetting)
        {
            GetDatabaseConnection().RegisterSetting(databaseSetting);
            return true;
        }

        public void MapSettingsPath(String path)
        {
            GetDatabaseConnection().SetSettingsPath(path);
        }

        public IEnumerable<string> GetDatabases()
        {
            return GetDatabaseConnection().GetDatabases();
        }

        private IDatabaseConnection GetDatabaseConnection()
        {
            return ObjectFactory.GetInstance<IDatabaseConnection>();
        }

        #endregion
    }
}