using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GenPres.Business.Domain
{
    interface ICalcalutorBusiness
    {
        object GetState();
        object GetUnitValue(PropertyInfo name, int iDose);
        object SetState(PropertyInfo name);
    }
}
