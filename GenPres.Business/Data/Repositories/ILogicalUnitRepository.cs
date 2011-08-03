using GenPres.Business.Domain.Patients;

namespace GenPres.Business.Data.DataAccess.Repositories
{
    public interface ILogicalUnitRepository
    {
        ILogicalUnit[] GetLogicalUnits();
    }
}
