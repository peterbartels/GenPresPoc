using System;
using GenPres.Business.WebService;
using StructureMap.Configuration.DSL;

namespace GenPres.Assembler
{
    public class GenFormWebServiceAssembler
    {
        private static Boolean _hasBeenCalled;
        private static Registry _registry;

        public static Registry RegisterDependencies()
        {
            if (_hasBeenCalled) return _registry;
            _registry = new Registry();
            
            _registry.For<IGenFormWebServices>().Use<GenFormWebServices>();

            _hasBeenCalled = true;
            return _registry;
        }
    }
}
