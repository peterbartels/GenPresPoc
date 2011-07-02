using GenPres.Business.Domain.Patient;

namespace GenPres.Business.Data.DataAccess.Repository
{
    public interface ILogicalUnitRepository
    {
        ILogicalUnit[] GetLogicalUnits();
    }
}
