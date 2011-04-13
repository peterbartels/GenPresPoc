using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enterprise.Data.MappingTypes
{
    public interface IMemberAccessor : IMemberGetter
    {
        void SetValue(object destination, object value);
    }
}
