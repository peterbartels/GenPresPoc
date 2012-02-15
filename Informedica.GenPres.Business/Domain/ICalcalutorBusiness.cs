using System.Reflection;

namespace Informedica.GenPres.Business.Domain
{
    interface ICalcalutorBusiness
    {
        object GetState();
        object GetUnitValue(PropertyInfo name, int iDose);
        object SetState(PropertyInfo name);
    }
}
