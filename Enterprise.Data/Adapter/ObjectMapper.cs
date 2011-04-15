using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enterprise.Data.AdapterTypes;

namespace Enterprise.Data
{
    public class ObjectMapper
    {
        internal IAdapter _adapter;

        internal ObjectMapper(IAdapter adapter)
        {
            _adapter = adapter;
        }

        internal void Map(object src, object dest)
        {
            List<IMappingType> mappingTypes = _adapter.LoadMappingTypes(src.GetType());
            
            if (mappingTypes == null) return;

            foreach (IMappingType mappingType in mappingTypes)
            {
                mappingType.Map(src, dest);
            }
        }
    }
}
