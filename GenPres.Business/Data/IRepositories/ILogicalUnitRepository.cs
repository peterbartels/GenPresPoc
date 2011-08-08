using GenPres.Business.Domain.Patients;

namespace GenPres.Business.Data.IRepositories
{
    public interface ILogicalUnitRepository
    {
        ILogicalUnit[] GetLogicalUnits();
    }
}
