using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enterprise.Service;

namespace GenPres.Business
{
    public class GenPresServiceProvider : ServiceProvider
    {
        private static GenPresServiceProvider _instance;
        private static readonly object _lock = new object();

        public static GenPresServiceProvider Create()
        {
            return _instance ?? (_instance =  CreateInstance(_instance, _lock));
        }
    }
}
