using System.Data;
using GenPres.Business.Domain.Patients;

namespace GenPres.Data.DAO.Mapper.Patients
{
    public class PdmsLogicalUnitMapper
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
