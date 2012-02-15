using System.Collections.ObjectModel;
using Informedica.GenPres.Business.Domain.Patients;

namespace Informedica.GenPres.Business.Data.IRepositories
{
    public interface IPdsmPatientRepository
    {
        ReadOnlyCollection<Patient> GetPatientsByLogicalUnitId(int logicalUnitId);
        Patient GetPatientByPid(string pid);
        Patient[] GetPatientsByLogicalUnitFromXml(int logicalUnitId);
    }
}
