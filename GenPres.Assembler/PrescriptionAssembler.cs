using System;
using GenPres.Business.Data.DataAccess.Repository;
using GenPres.DataAccess.Repository;
using StructureMap.Configuration.DSL;

namespace GenPres.Assembler
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
