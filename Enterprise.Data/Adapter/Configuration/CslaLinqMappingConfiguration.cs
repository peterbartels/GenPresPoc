using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enterprise.Data.AdapterTypes;
using Enterprise.Data.MappingTypes;


namespace Enterprise.Data.AdapterConfiguration
{
    public class CslaLinqMappingConfiguration<TSrc, TDest>
        : SimpleMappingConfiguration<TSrc, TDest>, ICslaLinqMappingExpression<TSrc, TDest>
    {

        public CslaLinqMappingConfiguration(CslaLinqAdapter<TSrc, TDest> adapter)
            : base(adapter)
        {
            
        }

        internal static CslaLinqMappingConfiguration<TSrc, TDest> GetMappingConfiguration(CslaLinqAdapter<TSrc, TDest> adapter)
        {
            CslaLinqMappingConfiguration<TSrc, TDest> mappingConfiguration = new CslaLinqMappingConfiguration<TSrc, TDest>(adapter);
            return mappingConfiguration;
        }
    }
}
