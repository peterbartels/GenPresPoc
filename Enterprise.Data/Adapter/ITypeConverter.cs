using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enterprise.Data
{
    public interface ITypeConverter<Type>
    {
        Type Convert(object value);
    }
}
