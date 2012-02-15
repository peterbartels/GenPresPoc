using System;
using System.Collections;

namespace Informedica.GenPres.xTest.Base.Mappers
{
    public class CustomEqualityComparer : IEqualityComparer
    {
        public virtual bool Equals(object x, object y)
        {
            if (x == null || y == null)
            {
                return false;
            }
            if (x is DateTime && y is DateTime)
            {
                return ((DateTime)x).Year == ((DateTime)y).Year;
            }
            return x.Equals(y);
        }

        public int GetHashCode(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
