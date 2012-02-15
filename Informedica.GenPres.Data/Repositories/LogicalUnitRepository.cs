using System.Linq;
using System.Xml.Linq;
using Informedica.GenPres.Business.Data.IRepositories;
using Informedica.GenPres.Business.Domain.Patients;
using Informedica.GenPres.Data.DAO.Mapper.Patients;
using Informedica.GenPres.Data.Managers;

namespace Informedica.GenPres.Data.Repositories
{
    public class LogicalUnitRepository : ILogicalUnitRepository
    {
        
        private PdmsLogicalUnitMapper _logicalUnitMapper = new PdmsLogicalUnitMapper();
        private XmlLogicalUnitMapper _xmlLogicalUnitMapper = new XmlLogicalUnitMapper();

        public LogicalUnit[] GetLogicalUnits()
        {
            return GetLogicalUnitsByXml();
        }

        public LogicalUnit[] GetLogicalUnitsByXml()
        {
            return GetLogicalUnitsFromXml();
        }

        public LogicalUnit[] GetLogicalUnitsBySql()
        {
            return GetLogicalUnitsFromDatabase();
        }

        public LogicalUnit[] GetLogicalUnitsFromXml()
        {
            var xmlDoc = XDocument.Parse(Informedica.GenPres.Data.Properties.Resources.patients);
            var logicalUnitsObj = from c in xmlDoc.Descendants("logicalunit")
                    select new
                    {
                        name = c.Attribute("name").Value
                    };

            var logicalUnits = new LogicalUnit[logicalUnitsObj.Count()];

            int id = 0;
            foreach (var logicalunit in logicalUnitsObj)
            {
                logicalUnits[id] = _xmlLogicalUnitMapper.MapFromBoToDao(id, logicalunit.name, LogicalUnit.NewLogicalUnit());
                id++;
            }

            return logicalUnits;
        }

        private LogicalUnit[] GetLogicalUnitsFromDatabase()
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
