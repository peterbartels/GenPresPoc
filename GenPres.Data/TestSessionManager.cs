using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GenPres.Business.Domain.Users;
using GenPres.Data;
using GenPres.Data.Connections;
using NHibernate;
using NHibernate.Context;

namespace GenPres.xTest.Base
{
    public class TestSessionManager : SessionManager
    {
        public static SessionManager Instance
        {
            get
            {
                if (_instance == null)
                    lock (LockThis)
                    {
                        if (_instance == null)
                        {
                            var instance = new TestSessionManager();
                            Thread.MemoryBarrier();
                            _instance = instance;
                            Thread.MemoryBarrier();
                        }
                    }
                return _instance;
            }
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

        public static ISession InitSession()
        {
            if (_currentSession == null)
            {
                _currentSession = _factory.OpenSession();
                SessionFactoryCreator.BuildSchema(_currentSession);
                CurrentSessionContext.Bind(_currentSession);
                Instance.InsertData();
            }

            return _currentSession;
        }

        public override ISessionFactory InitSessionFactory(DatabaseConnection.DatabaseName databaseName, bool exposeConfiguration)
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
