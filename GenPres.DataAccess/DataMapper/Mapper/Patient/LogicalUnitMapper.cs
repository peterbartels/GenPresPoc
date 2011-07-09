using System.Data;
using GenPres.Business.Domain.Patient;

namespace GenPres.DataAccess.DataMapper.Mapper.Patient
{
    public class LogicalUnitMapper
    {
        public ILogicalUnit MapFromBoToDao(object daoObj, ILogicalUnit logicalUnitBo)
        {
            var logicalUnitDao = (DataRow)daoObj;
            logicalUnitBo.Id = int.Parse(logicalUnitDao["LogicalUnitID"].ToString());
            logicalUnitBo.Name = logicalUnitDao["Name"].ToString();
            return logicalUnitBo;
        }
    }
}
