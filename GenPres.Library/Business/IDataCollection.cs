using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business
{
    interface IDataCollection
    {
        object GetParent();
        Csla.Core.IBusinessObject[] GetList();
        void AddItem(object item);
    }
}
