using System;
using Informedica.GenPres.Business.Data.IRepositories;
using Informedica.GenPres.Data.Repositories;
using StructureMap.Configuration.DSL;

namespace Informedica.GenPres.Assembler
{
    class PrescriptionAssembler
    {
        private static Boolean _hasBeenCalled;
        private static Registry _registry;

        public static Registry RegisterDependencies()
        {
            if (_hasBeenCalled) return _registry;
            _registry = new Registry();

            _registry.For<IPrescriptionRepository>().Use<PrescriptionRepository>();

            _hasBeenCalled = true;
            return _registry;
        }
    }
}
