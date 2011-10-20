using System;
using System.Globalization;
using System.Threading;
using GenPres.Data;
using GenPres.Data.Connections;
using GenPres.Data.Managers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using StructureMap;

namespace GenPres.Assembler
{
    public class GenPresApplication
    {
        private static ISessionFactory _factory;
        private static readonly Object LockThis = new object();

        [ThreadStatic]
        private static GenPresApplication _instance;
        
        public static GenPresApplication Instance
        {
            get
            {
                if (_instance == null)
                    lock (LockThis)
                    {
                        if (_instance == null)
                        {
                            var instance = new GenPresApplication();
                            Thread.MemoryBarrier();
                            _instance = instance;
                            Thread.MemoryBarrier();
                        }
                    }
                return _instance;
            }
        }
        
        public static ISessionFactory SessionFactory
        {
            get { return Instance.SessionFactoryFromInstance; }
        }

        private ISessionFactory SessionFactoryFromInstance
        {
            get
            {
                return SessionManager.Instance.InitSessionFactory(DatabaseConnection.DatabaseName.GenPresTest, true);
            }
        }

        public static void Initialize()
        {
            InitializeObjectFactory();
            SetCultureInfo();
        }

        private static void InitializeObjectFactory()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry(PatientAssembler.RegisterDependencies());
                x.AddRegistry(UserAssembler.RegisterDependencies());
                x.AddRegistry(GenFormWebServiceAssembler.RegisterDependencies());
                x.AddRegistry(PrescriptionAssembler.RegisterDependencies());
                x.AddRegistry(DatabaseRegistrationAssembler.RegisterDependencies());
            });
        }

        public static void SetCultureInfo()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }
    }
}
