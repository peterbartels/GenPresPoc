using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enterprise.Data;

namespace xData.Test.Unit
{
    public class TestDataManager : DataManager<DBTestDataContext>
    {
        public TestDataManager()
            : base(TestDatabaseConnection.GetConnectionString(TestDatabaseConnection.DatabaseName.TestDatabase))
        { }

        public static TestDataManager GetManager()
        {
            TestDataManager mgr = new TestDataManager();
            return mgr;
        }
    }
}
