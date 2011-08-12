using System;
using GenPres.Business.Data.IRepositories;
using GenPres.Data.Repositories;
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

            _registry.For<IUserRepository>().Use<UserSqlRepository>();

            _hasBeenCalled = true;
            return _registry;
        }
    }
}
