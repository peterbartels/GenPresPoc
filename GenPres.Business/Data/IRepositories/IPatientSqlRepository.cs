using GenPres.Business.Domain.Patients;

namespace GenPres.Business.Data.IRepositories
{
    public interface IPatientSqlRepository : ISQLRepository<IPatient>
    {
        IPatient GetByPid(string pid);
        bool PatientExists(string patientId);
    }
}
