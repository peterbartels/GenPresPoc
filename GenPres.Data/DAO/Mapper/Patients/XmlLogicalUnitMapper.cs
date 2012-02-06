using System.Data;
using GenPres.Business.Domain.Patients;

namespace GenPres.Data.DAO.Mapper.Patients
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
