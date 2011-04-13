using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enterprise.Data
{
    public interface IDataObject
    {
        void MapToDataObject(object obj);
        void MapToBusinessObject(object obj);
    }
}
