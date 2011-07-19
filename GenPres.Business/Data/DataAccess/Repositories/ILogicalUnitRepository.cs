using GenPres.Business.Domain.PatientDomain;

namespace GenPres.Business.Data.DataAccess.Repositories
{
    public interface ILogicalUnitRepository
    {
        ILogicalUnit[] GetLogicalUnits();
    }
}
