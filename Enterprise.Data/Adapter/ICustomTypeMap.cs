using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enterprise.Data
{
    public interface ICustomTypeMap
    {
        void Map(object src, object dest);
    }
}
