using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enterprise.Data.AdapterTypes
{
    public interface IMappingType
    {
        void Map(object src, object dest);
    }
}
