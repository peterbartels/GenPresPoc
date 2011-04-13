using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Enterprise.Data.MappingTypes
{
    public interface IMemberGetter
    {
        object GetValue(object source);
    }
}
