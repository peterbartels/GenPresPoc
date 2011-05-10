using System.Collections.Generic;
using GenPres.Business.Domain;

namespace GenPres.Business.Data.DataAccess.Repository
{
    public interface IPatientRepository
    {
        List<IPatient> GetPatientsByLogicalUnitId(int logicalUnitId);
    }
}
