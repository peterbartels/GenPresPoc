using Informedica.GenPres.Data.Connections;
using NHibernate;
using NHibernate.Context;

namespace Informedica.GenPres.Data
{
    public class TestSessionManager : SessionManager
    {   
        public TestSessionManager()
        {
            Initialize();
        }

        private void Initialize()
        {
            //ObjectFactory.Configure(x => x.For<ISessionFactory>().HybridHttpOrThreadLocalScoped().Use());
        }

        public static void CloseSession()
        {
            CurrentSessionContext.Unbind(_factory);
            _currentSession = null;
        }
        public static void Init()
        {
            InitSession();
        }

        public static void Close()
        {
            CloseSession();
        }

        public ISessionFactory InitTestSessionFactory()
        {
            return InitSessionFactory(DatabaseConnection.DatabaseName.GenPresTest, true);
        }

        public static ISession InitSession()
        {
            _currentSession = _factory.OpenSession();
            SessionFactoryCreator.BuildSchema(_currentSession);
            CurrentSessionContext.Bind(_currentSession);
            InsertData();
            return _currentSession;
        }

        public static ISessionFactory InitSessionFactory(DatabaseConnection.DatabaseName databaseName, bool exposeConfiguration)
        {
            //HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            if (_factory == null)
            {
                _factory = SessionFactoryCreator.CreateSessionFactory(databaseName);
            }
            
            return _factory;
        }
    }
}
