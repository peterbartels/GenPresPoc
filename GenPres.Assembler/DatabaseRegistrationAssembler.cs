using System;
using GenPres.Business.Domain.DatabaseDomain;
using GenPres.Business.Service;
using GenPres.DataAccess.Databases;
using StructureMap.Configuration.DSL;

namespace GenPres.Assembler
{
    class DatabaseRegistrationAssembler
    {
        private static Boolean _hasBeenCalled;
        private static Registry _registry;

        public static Registry RegisterDependencies()
        {
            if (_hasBeenCalled) return _registry;
            _registry = new Registry();

            _registry.For<IDatabaseServices>().Use<DatabaseServices>();
            _registry.For<IDatabaseSetting>().Use<DatabaseSetting>();
            _registry.For<IDatabaseConnection>().Use<DatabaseConnection>();

            _hasBeenCalled = true;
            return _registry;
        }
    }
}
