using System.Collections.Generic;
using GenPres.Business.Domain;
using GenPres.Business.Domain.Patient;

namespace GenPres.Business.Data.DataAccess.Repository
{
    public interface IPatientRepository
    {
        List<IPatient> GetPatientsByLogicalUnitId(int logicalUnitId);
    }
}
