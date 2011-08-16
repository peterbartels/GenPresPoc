using System;
using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Patients;
using GenPres.Data.DAO.Mapper.Patient;
using GenPres.Data.Managers;

namespace GenPres.Data.Repositories
{
    public class LogicalUnitRepository : ILogicalUnitRepository
    {
        
        private LogicalUnitMapper _logicalUnitMapper = new LogicalUnitMapper();

        public LogicalUnit[] GetLogicalUnits()
        {
            var sqlResult = PDMSDataRetriever.ExecuteSQL("SELECT * FROM LogicalUnits WHERE LogicalUnitID IN(1,50,57)");
            
            LogicalUnit[] logicalUnits = new LogicalUnit[sqlResult.Tables[0].Rows.Count];

            for (int i = 0; i < sqlResult.Tables[0].Rows.Count; i++)
            {
                logicalUnits[i] = _logicalUnitMapper.MapFromBoToDao(sqlResult.Tables[0].Rows[i], LogicalUnit.NewLogicalUnit());
            }
            return logicalUnits;
        }
    }
}
