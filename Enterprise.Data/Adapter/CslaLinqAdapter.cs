using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enterprise.Data.AdapterConfiguration;
using Enterprise.Data.AdapterTypes;

namespace Enterprise.Data
{
    public class CslaLinqAdapter : SimpleAdapter
    {
        public CslaLinqAdapter() : base() { }

        internal List<IMappingType> LoadMappingTypes(Type source)
        {
            return (
                from i in _mappingConfiguration where i.GetSourceType() == source select i.GetMappingTypes()
            ).SingleOrDefault<List<IMappingType>>();
        }

        public ICslaLinqMappingExpression<TSource, TDestination> ConfigureMapping<TSource, TDestination>()
        {
            //Get the mapping configuration
            CslaLinqMappingConfiguration<TSource, TDestination> mappingConfiguration = CslaLinqMappingConfiguration<TSource, TDestination>.GetMappingConfiguration(this);

            //Add the configuration to the mappingconfiguration list
            _mappingConfiguration.Add(mappingConfiguration);

            return mappingConfiguration;
        }

        internal IMappingConfiguration GetMappingConfiguration(Type sourceType)
        {
            return
                (from i in _mappingConfiguration where i.GetSourceType() == sourceType select i).LastOrDefault<IMappingConfiguration>();
        }
    }
}
