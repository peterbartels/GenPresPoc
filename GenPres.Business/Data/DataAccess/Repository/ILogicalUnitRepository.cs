using GenPres.Business.Domain;

namespace GenPres.Business.Data.DataAccess.Repository
{
    public interface ILogicalUnitRepository
    {
        ILogicalUnit[] GetLogicalUnits();
    }
}
