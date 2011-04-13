using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business.Data
{
    public class DtoFactory
    {
        public static T Create<T>() where T : IDto, new()
        {
            return new T();
        }
    }
}
