using System;
using System.Threading;
using GenPres.Data.Managers;
using NHibernate;
using NHibernate.Context;
using StructureMap;

namespace GenPres.Data
{
    public class SessionFactoryManager
    {
        private static ISessionFactory _factory;
        private static readonly Object LockThis = new object();

        [ThreadStatic]
        private static SessionFactoryManager _instance;

        public static SessionFactoryManager Instance
        {
            get
            {
                if (_instance == null)
                    lock (LockThis)
                    {
                        if (_instance == null)
                        {
                            var instance = new SessionFactoryManager();
                            Thread.MemoryBarrier();
                            _instance = instance;
                            Thread.MemoryBarrier();
                        }
                    }
                return _instance;
            }
        }

        public ISessionFactory InitSessionFactory<TMappingType>()
        {
            if (_factory == null)
            {
                _factory = SessionFactoryCreator.CreateSessionFactory<TMappingType>();
                CurrentSessionContext.Bind(_factory.OpenSession());
            }
            return _factory;
        }

        public void CloseSessionFactory()
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

        private static ISessionFactory CreateSessionFactory()
        {
            return SessionFactoryCreator.CreateSessionFactory<Data.Mappings.PrescriptionMap>();
        }
    }
}
