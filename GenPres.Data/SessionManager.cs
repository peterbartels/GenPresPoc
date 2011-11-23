using System;
using System.Threading;
using GenPres.Business;
using GenPres.Business.Domain.Users;
using GenPres.Data.Connections;
using NHibernate;
using NHibernate.Context;

namespace GenPres.Data
{
    public class SessionManager
    {
        protected static ISessionFactory _factory;

        protected static readonly Object LockThis = new object();

        protected static SessionManager _instance;
        
        public static SessionManager Instance
        {
            get
            {
                if (_instance == null)
                    lock (LockThis)
                    {
                        if (_instance == null)
                        {
                            var instance = new SessionManager();
                            Thread.MemoryBarrier();
                            _instance = instance;
                            Thread.MemoryBarrier();
                        }
                    }
                return _instance;
            }
        }

        public void InsertData()
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

        public virtual void InitTestSessionFactory()
        {
            InitSessionFactory(DatabaseConnection.DatabaseName.GenPresTest, true);
        }

        public virtual ISessionFactory InitSessionFactory(DatabaseConnection.DatabaseName databaseName, bool exposeConfiguration)
        {
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

            if (_factory == null)
            {
                _factory = SessionFactoryCreator.CreateSessionFactory(databaseName);
            }

            //SessionFactoryCreator.BuildSchema(_currentSession);
            //var session = _factory.OpenSession();
            //CurrentSessionContext.Bind(session);
            //InsertData();
            //CurrentSessionContext.Unbind(_factory);
            //session.Close();
            //if (exposeConfiguration)
            {
                //var session = _factory.OpenSession();
                //CurrentSessionContext.Bind(session);
                //SessionFactoryCreator.BuildSchema(session);
                //InsertData();
                //CurrentSessionContext.Unbind(_factory);
                //session.Close();
            }

            return _factory;
        }

        public void CloseSession()
        {
            var session = CurrentSessionContext.Unbind(_factory);
            session.Close();
        }

        public static ISessionFactory SessionFactory
        {
            get { return Instance.SessionFactoryFromInstance; }
        }

        public ISessionFactory SessionFactoryFromInstance
        {
            get { return _factory; }
        }

        private static ISessionFactory CreateSessionFactory(DatabaseConnection.DatabaseName databaseName, bool exposeConfiguration)
        {
            return SessionFactoryCreator.CreateSessionFactory(databaseName);
        }
    }
}
