using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enterprise.Data.AdapterConfiguration;
using Enterprise.Data.AdapterTypes;

namespace Enterprise.Data
{
    public interface IAdapter
    {
        IMappingConfiguration GetMappingConfiguration(Type sourceType);
        List<IMappingType> LoadMappingTypes(Type source);
    }
}
