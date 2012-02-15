using System;
using Informedica.GenPres.Business.Data.IRepositories;
using Informedica.GenPres.Data.Repositories;
using StructureMap.Configuration.DSL;

namespace Informedica.GenPres.Assembler
{
    public class PatientAssembler
    {
        private static Boolean _hasBeenCalled;
        private static Registry _registry;

        public static Registry RegisterDependencies()
        {
            if (_hasBeenCalled) return _registry;
            _registry = new Registry();

            _registry.For<ILogicalUnitRepository>().Use<LogicalUnitRepository>();
            _registry.For<IPdsmPatientRepository>().Use<PdmsRepository>();
            _registry.For<IPatientRepository>().Use<PatientRepository>();
            
            _hasBeenCalled = true;
            return _registry;
        }
        
    }
}
