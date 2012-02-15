using Informedica.GenPres.Business.Domain.Patients;

namespace Informedica.GenPres.Business.Data.IRepositories
{
    public interface ILogicalUnitRepository
    {
        LogicalUnit[] GetLogicalUnits();
        LogicalUnit[] GetLogicalUnitsFromXml();
    }
}
