using System.Data;
using GenPres.Business.Domain.Patients;

namespace GenPres.Data.DAO.Mapper.Patient
{
    public class LogicalUnitMapper
    {
        public LogicalUnit MapFromBoToDao(object daoObj, LogicalUnit logicalUnitBo)
        {
            var logicalUnitDao = (DataRow)daoObj;
            logicalUnitBo.Id = int.Parse(logicalUnitDao["LogicalUnitID"].ToString());
            logicalUnitBo.Name = logicalUnitDao["Name"].ToString();
            return logicalUnitBo;
        }
    }
}
