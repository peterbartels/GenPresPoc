using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace GenPres.Assembler
{
    public class GenPresApplication
    {
        public static void Initialize()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry(PatientAssembler.RegisterDependencies());
                x.AddRegistry(UserAssembler.RegisterDependencies());
                x.AddRegistry(GenFormAssembler.RegisterDependencies());
                x.AddRegistry(PrescriptionAssembler.RegisterDependencies());
            });
        }
    }
}
