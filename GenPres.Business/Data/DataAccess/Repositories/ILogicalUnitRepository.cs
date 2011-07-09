using GenPres.Business.Domain.Patient;

namespace GenPres.Business.Data.DataAccess.Repositories
{
    public interface ILogicalUnitRepository
    {
        ILogicalUnit[] GetLogicalUnits();
    }
}
