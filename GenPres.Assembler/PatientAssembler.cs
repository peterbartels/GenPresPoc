using GenPres.Business.Data.DataAccess.Repository;
using GenPres.DataAccess.Repository;
using GenPres.Business.ServiceProvider;

namespace GenPres.Assembler
{
    public class PatientAssembler
    {
        public static void RegisterDependencies()
        {
            var repository = (IUserRepository)new UserRepository();
            DalServiceProvider.Instance.RegisterInstanceOfType(repository);
        }
    }
}
