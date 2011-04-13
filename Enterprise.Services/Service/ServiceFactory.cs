using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enterprise.Service
{
    public class ServiceFactory
    {
        public static T Create<T>() where T : IApplicationService, new()
        {
            return new T();
        }
    }
}
