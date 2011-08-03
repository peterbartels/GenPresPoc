using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Domain.Patients;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Business.Domain.Units;
using GenPres.Business.Domain.Users;
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
