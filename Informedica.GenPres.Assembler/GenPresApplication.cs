using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Informedica.GenPres.Data;
using Informedica.GenPres.Data.Connections;
using NHibernate;
using StructureMap;

namespace Informedica.GenPres.Assembler
{
    public class GenPresApplication
    {
        private static readonly Object LockThis = new object();

        [ThreadStatic]
        private static GenPresApplication _instance;

        private static ISessionFactory _sessionFactory;
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
        
        public static void CloseSessionFactory()
        {
            TestSessionManager.CloseSession();
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


        public static ISessionFactory GetSessionFactory(DatabaseConnection.DatabaseName environment)
        {
            if(_sessionFactory == null)
            {
                _sessionFactory = TestSessionManager.InitSessionFactory(DatabaseConnection.DatabaseName.GenPresTest, true);
            }
            return _sessionFactory;  
            //Factories.Add(environment.ToString(), SessionManager.InitSessionFactory(DatabaseConnection.DatabaseName.GenPresTest, true));
            //}

            //return Factories[environment.ToString()];
        }
    }
}
