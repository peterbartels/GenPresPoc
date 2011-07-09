
using System;

namespace GenPres.Business.Domain
{
    public static class ObjectFactory<T> where T : ISavable
    {
        
        public static T New()
        {
            T obj = Activator.CreateInstance<T>();
            obj.OnCreate();
            obj.OnNew();
            obj.IsNew = true;
            return obj;
        }

        public static T InitExisting()
        {
            T obj = Activator.CreateInstance<T>();
            obj.OnCreate();
            obj.OnInitExisting();
            obj.IsNew = false;
            return obj;
        }
        public static T Create(bool isNew)
        {
            if(isNew)
            {
                return New();
            }
            return InitExisting();
        }
    }
}
