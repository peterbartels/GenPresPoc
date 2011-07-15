
using System;

namespace GenPres.Business.Domain
{
    public static class ObjectFactory 
    {
        
        public static T New<T>()
            where T:class,ISavable
        {
            T obj = Activator.CreateInstance<T>();
            obj.OnCreate();
            obj.OnNew();
            obj.IsNew = true;
            return obj;
        }
        
        public static object InitExisting(Type t)
        {
            object obj = Activator.CreateInstance(t);
            if(obj is ISavable)
            {
                ((ISavable)obj).OnCreate();
                ((ISavable)obj).OnInitExisting();
                ((ISavable)obj).IsNew = false;    
            }
            
            return obj;
        }
        public static T InitExisting<T>()
            where T : class,ISavable
        {
            T obj = Activator.CreateInstance<T>();
            obj.OnCreate();
            obj.OnInitExisting();
            obj.IsNew = false;
            return obj;
        }
        public static T Create<T>(bool isNew)
            where T : class,ISavable
        {
            if(isNew)
            {
                return New<T>();
            }
            return InitExisting<T>();
        }
    }
}
