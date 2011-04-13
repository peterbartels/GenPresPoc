using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enterprise.Data.Access
{
    public interface IObjectRepository
    {
        IDataObject GetById();
    }
}
