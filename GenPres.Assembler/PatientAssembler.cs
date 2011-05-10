using GenPres.Business.Data.DataAccess.Repository;
using GenPres.DataAccess.Repository;
using GenPres.Business.ServiceProvider;

namespace GenPres.Assembler
{
    public class PatientAssembler
    {
        public static void RegisterDependencies()
        {
            var logicalUnitRepository = (ILogicalUnitRepository)new LogicalUnitRepository();
            DalServiceProvider.Instance.RegisterInstanceOfType(logicalUnitRepository);

            var patientRepository = (IPatientRepository)new PatientRepository();
            DalServiceProvider.Instance.RegisterInstanceOfType(patientRepository);
        }
    }
}
