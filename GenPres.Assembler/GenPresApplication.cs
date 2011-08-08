using GenPres.Data.Managers;
using StructureMap;

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
                x.AddRegistry(DatabaseRegistrationAssembler.RegisterDependencies());
                x.AddRegistry(BusinessAssembler.RegisterDependencies());
            });

            ObjectFactory.Configure(x => x.For<IDataContextManager>().Use<GenPresDataContextManager>());
        }
    }
}
