using GenPres.Business.Domain;
using System.Data;
using GenPres.Business.Domain.Patient;

namespace GenPres.Business.Data.DataAccess.Mapper.Patient
{
    public class LogicalUnitMapper
    {
        public ILogicalUnit MapDaoToBusinessObject(object daoObj, ILogicalUnit logicalUnitBo)
        {
            var logicalUnitDao = (DataRow)daoObj;
            logicalUnitBo.Id = int.Parse(logicalUnitDao["LogicalUnitID"].ToString());
            logicalUnitBo.Name = logicalUnitDao["Name"].ToString();
            return logicalUnitBo;
        }
    }
}
