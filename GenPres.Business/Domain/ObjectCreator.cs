using System;
using SM = StructureMap;

namespace GenPres.Business.Domain
{
    public static class ObjectCreator 
    {
        
        public static T New<T>()
            where T:ISavable
        {
            var obj = (T)SM.ObjectFactory.GetInstance(typeof(T));
            return obj;
        }
        
        public static object InitExisting(Type t)
        {
            object obj;
            if(t.GetInterface(typeof(ISavable).FullName) != null)
            {
                obj = SM.ObjectFactory.GetInstance(t);
            }else
            {
                obj = Activator.CreateInstance(t);    
            }
            return obj;
        }
        public static T InitExisting<T>()
            where T : ISavable
        {
            var obj = (T)SM.ObjectFactory.GetInstance(typeof(T));
            return obj;
        }
        public static T Create<T>(bool isNew)
            where T : ISavable
        {
            return isNew ? New<T>() : InitExisting<T>();
        }
    }
}
