using System;
using GenPres.Business.Data.DataAccess.Repositories;
using GenPres.DataAccess.Repositories;
using StructureMap.Configuration.DSL;

namespace GenPres.Assembler
{
    public class UserAssembler
    {
        private static Boolean _hasBeenCalled;
        private static Registry _registry;

        public static Registry RegisterDependencies()
        {
            if (_hasBeenCalled) return _registry;
            _registry = new Registry();

            _registry.For<IUserRepository>().Use<UserRepository>();

            _hasBeenCalled = true;
            return _registry;
        }
    }
}
