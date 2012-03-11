using System;
using Informedica.GenPres.Business;
using Informedica.GenPres.Business.Domain.Users;
using Informedica.GenPres.Data.Connections;
using NHibernate;
using NHibernate.Context;

namespace Informedica.GenPres.Data
{
    public class SessionManager
    {
        protected static ISessionFactory _factory;

        protected static readonly Object LockThis = new object();

        public static void InsertData()
        {
            var u = User.NewUser();
            u.UserName = "Peter";
            u.PassCrypt = AuthenticationFunctions.MD5("Secret");
            u.Save();
        }

        protected static ISession _currentSession;

        public static ISession InitSession()
        {
            if (_currentSession == null)
            {
                _currentSession = _factory.OpenSession();
                CurrentSessionContext.Bind(_currentSession);
            }

            return _currentSession;
        }

        
        public static ISessionFactory InitSessionFactory(DatabaseConnection.DatabaseName databaseName, bool exposeConfiguration)
        {
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

            if (_factory == null)
            {
                _factory = SessionFactoryCreator.CreateSessionFactory(databaseName);
            }

            var session = _factory.OpenSession();
            CurrentSessionContext.Bind(session);
            if(exposeConfiguration)
            {
                SessionFactoryCreator.BuildSchema(session);  
            }
            return _factory;
        }

        public static void CloseSession()
        {
            var session = CurrentSessionContext.Unbind(_factory);
            session.Close();
        }

        public static ISessionFactory SessionFactory
        {
            get { return SessionFactoryFromInstance; }
        }

        public static ISessionFactory SessionFactoryFromInstance
        {
            get { return _factory; }
        }

        private static ISessionFactory CreateSessionFactory(DatabaseConnection.DatabaseName databaseName, bool exposeConfiguration)
        {
            return SessionFactoryCreator.CreateSessionFactory(databaseName);
        }
    }
}
