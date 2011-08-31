using System;
using System.Threading;
using GenPres.Business.Domain.Users;
using GenPres.Data.Connections;
using GenPres.Data.Managers;
using NHibernate;
using NHibernate.Context;
using StructureMap;

namespace GenPres.Data
{
    public class SessionManager
    {
        private static ISessionFactory _factory;

        private static readonly Object LockThis = new object();

        private static SessionManager _instance;

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

        public ISessionFactory InitSessionFactory(DatabaseConnection.DatabaseName databaseName, bool exposeConfiguration)
        {
            if (_factory == null)
            {
                _factory = SessionFactoryCreator.CreateSessionFactory(databaseName);
            }
            var session = _factory.OpenSession();
            CurrentSessionContext.Bind(session);

            SessionFactoryCreator.BuildSchema(session);

            var u = User.NewUser();
            u.UserName = "test";
            u.PassCrypt = "0cbc6611f5540bd0809a388dc95a615b";
            //u.Save();
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

        [Obsolete]
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
