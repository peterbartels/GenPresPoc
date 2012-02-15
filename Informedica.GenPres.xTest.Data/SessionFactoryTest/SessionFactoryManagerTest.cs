using System.Data;
using System.Data.SQLite;
using Informedica.GenPres.Data;
using Informedica.GenPres.xTest.Data.SessionFactoryTest.Fixtures.Class;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Context;

namespace Informedica.GenPres.xTest.Data.SessionFactoryTest
{
    /// <summary>
    /// Summary description for NHibernatePersitencyAndMappingTest
    /// </summary>
    [TestClass]
    public class SessionFactoryManagerTest
    {
        #region Constructor and Context

        public SessionFactoryManagerTest() { }

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        #endregion
        
        [TestMethod]
        public void ThatASessionFactoryCanBeCreated()
        {
            var sessionFactory = SessionFactoryManager.GetSessionFactory("Test");
            Assert.IsNotNull(sessionFactory);
        }

        [TestMethod]
        public void ThatASessionFactoryCanBuildASqliteDatabaseShema()
        {
            var entityClass = new EntityClass();
            var sessionFactory = SessionFactoryManager.GetSessionFactory("Test");
            var session = sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
            SessionFactoryManager.BuildSchema("Test", session);
            
            var tables =  ((SQLiteConnection)session.Connection).GetSchema("Columns", new[] { null, null, "EntityClass" });

            foreach (DataRow test in tables.Rows)
            {
                var d = (string) test["column_name"];
            }  
            CurrentSessionContext.Unbind(sessionFactory);
            session.Close();
            
        }
    }
}
