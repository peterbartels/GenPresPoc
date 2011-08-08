using System.Collections.Generic;
using GenPres.Business.Domain.Patients;

namespace GenPres.Business.Data.IRepositories
{
    public interface IPdsmRepository
    {
        List<IPatient> GetPatientsByLogicalUnitId(int logicalUnitId);
    }
}
