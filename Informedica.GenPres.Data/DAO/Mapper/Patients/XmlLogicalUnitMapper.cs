using Informedica.GenPres.Business.Domain.Patients;

namespace Informedica.GenPres.Data.DAO.Mapper.Patients
{
    public class XmlLogicalUnitMapper
    {
        public LogicalUnit MapFromBoToDao(int id, string name, LogicalUnit logicalUnitBo)
        {
            logicalUnitBo.Id = id;
            logicalUnitBo.Name = name;
            return logicalUnitBo;
        }
    }
}
