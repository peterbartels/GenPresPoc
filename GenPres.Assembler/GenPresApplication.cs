using System;
using System.Threading;
using GenPres.Data;
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

        public static void Initialize()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry(PatientAssembler.RegisterDependencies());
                x.AddRegistry(UserAssembler.RegisterDependencies());
                x.AddRegistry(GenFormAssembler.RegisterDependencies());
                x.AddRegistry(PrescriptionAssembler.RegisterDependencies());
                x.AddRegistry(DatabaseRegistrationAssembler.RegisterDependencies());
                x.AddRegistry(BusinessAssembler.RegisterDependencies());
            });

            ObjectFactory.Configure(x => x.For<IDataContextManager>().Use<GenPresDataContextManager>());
        }
    }
}
