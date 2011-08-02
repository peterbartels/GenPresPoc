using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Domain.PatientDomain;
using GenPres.Business.Domain.PrescriptionDomain;
using GenPres.Business.Domain.UnitDomain;
using GenPres.Business.Domain.UserDomain;
using StructureMap.Configuration.DSL;

namespace GenPres.Assembler
{
    public class BusinessAssembler
    {
        private static Boolean _hasBeenCalled;
        private static Registry _registry;

        public static Registry RegisterDependencies()
        {
            if (_hasBeenCalled) return _registry;
            _registry = new Registry();

            _registry.For<IPrescription>().Use<Prescription>();
            _registry.For<IUser>().Use<User>();
            _registry.For<IPatient>().Use<Patient>();
            _registry.For<IDrug>().Use<Drug>();
            _registry.For<IUnitValue>().Use<UnitValue>();
            _registry.For<ILogicalUnit>().Use<LogicalUnit>();

            _hasBeenCalled = true;
            return _registry;
        }
    }
}
