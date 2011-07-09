using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Data.DataAccess;
using GenPres.Business.Data.DataAccess.Repositories;
using GenPres.Business.Domain;
using GenPres.Business;
using GenPres.Business.Domain.Patient;
using GenPres.DataAccess.DataMapper.Mapper.Patient;

namespace GenPres.DataAccess.Repositories
{
    public class LogicalUnitRepository : ILogicalUnitRepository
    {
        private LogicalUnitMapper _logicalUnitMapper = new LogicalUnitMapper();

        public ILogicalUnit[] GetLogicalUnits()
        {
            var sqlResult = PDMSDataRetriever.ExecuteSQL("SELECT * FROM LogicalUnits WHERE LogicalUnitID IN(1,50,57)");
            
            ILogicalUnit[] logicalUnits = new ILogicalUnit[sqlResult.Tables[0].Rows.Count];

            for (int i = 0; i < sqlResult.Tables[0].Rows.Count; i++)
            {
                logicalUnits[i] = _logicalUnitMapper.MapFromBoToDao(sqlResult.Tables[0].Rows[i], LogicalUnit.NewLogicalUnit());
            }
            return logicalUnits;
        }
    }
}
