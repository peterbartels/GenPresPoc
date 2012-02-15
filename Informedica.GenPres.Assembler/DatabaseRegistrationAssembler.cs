using System;
using Informedica.GenPres.Business.Domain.Databases;
using Informedica.GenPres.Data.Connections;
using Informedica.GenPres.Service;
using StructureMap.Configuration.DSL;

namespace Informedica.GenPres.Assembler
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
