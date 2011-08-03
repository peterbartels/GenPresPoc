using System.Data;
using GenPres.Business.Domain.Patients;

namespace GenPres.DataAccess.DAO.Mapper.Patient
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
