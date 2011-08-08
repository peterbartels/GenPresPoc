using GenPres.Business.Domain.Patients;

namespace GenPres.Business.Data.IRepositories
{
    public interface IPatientRepository : IRepository<IPatient>
    {
        IPatient GetByPid(string pid);
    }
}
