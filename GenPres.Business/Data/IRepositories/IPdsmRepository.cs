using System.Collections.Generic;
using System.Collections.ObjectModel;
using GenPres.Business.Domain.Patients;

namespace GenPres.Business.Data.IRepositories
{
    public interface IPdsmRepository
    {
        ReadOnlyCollection<Patient> GetPatientsByLogicalUnitId(int logicalUnitId);
        Patient GetPatientByPid(string pid);
    }
}
