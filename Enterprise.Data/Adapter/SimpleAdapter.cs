using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enterprise.Data.AdapterConfiguration;
using Enterprise.Data.AdapterTypes;

namespace Enterprise.Data
{
    public class SimpleAdapter<TSrc, TDest> : IAdapter
    {
        protected List<IMappingConfiguration> _mappingConfiguration = new List<IMappingConfiguration>();
        protected ObjectMapper _objectMapper;

        public SimpleAdapter()
        {
            _objectMapper = new ObjectMapper(this);
        }

        public List<IMappingType> LoadMappingTypes(Type source)
        {
            return (
                from i in _mappingConfiguration where i.GetSourceType() == source select i.GetMappingTypes()
            ).SingleOrDefault<List<IMappingType>>();
        }

        public ISimpleMappingExpression<TSrc, TDest> ConfigureMapping()
        {
            //Get the mapping configuration
            SimpleMappingConfiguration<TSrc, TDest> mappingConfiguration = SimpleMappingConfiguration<TSrc, TDest>.GetMappingConfiguration(this);
            
            //Add the configuration to the mappingconfiguration list
            _mappingConfiguration.Add(mappingConfiguration);

            return mappingConfiguration;
        }

        public IMappingConfiguration GetMappingConfiguration(Type sourceType)
        {
            return 
                (from i in _mappingConfiguration where i.GetSourceType() == sourceType select i).LastOrDefault<IMappingConfiguration>();
        } 

        //Perform the actual mapping
        public void Map(object source, object destination)
        {
            _objectMapper.Map(source, destination);
        }
    }
}
