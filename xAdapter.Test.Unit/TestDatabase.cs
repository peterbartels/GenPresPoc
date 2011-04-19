using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xData.Test.Unit
{
    public static class TestDatabaseConnection
    {
        public enum DatabaseName
        {
            TestDatabase
        }
        public static string GetConnectionString(DatabaseName database)
        {
            string connection = string.Empty;

            switch (database)
            {
                case DatabaseName.TestDatabase:
                    connection = "";
                    break;    
                default:
                    throw new Exception("Database not found");
            }
            return connection;
        }
    }

}
