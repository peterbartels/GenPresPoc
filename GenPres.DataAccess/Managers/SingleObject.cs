using System;
using System.Collections.Generic;
using System.Linq;

namespace GenPres.DataAccess.Managers
{
    public interface ISingleObject<T>
    {
        bool IsAvailable { get; }
        bool FoundMultiple { get; }
        T Object { get; }
    }

    public class SingleObject<T> : ISingleObject<T>
    {
        private readonly T _object;

        private SingleObject(T obj)
        {
            _object = obj;
        }

        public static ISingleObject<T> GetSingleObject(IEnumerable<T> objectList)
        {
            if (objectList.Count() == 0) return new SingleObjectUnavailable<T>();
            if (objectList.Count() > 1) return new SingleObjectList<T>();
            return new SingleObject<T>(objectList.First());
        }

        public T Object
        {
            get { return _object; }
        }

        public bool IsAvailable
        {
            get { return true; }
        }
        public bool FoundMultiple
        {
            get { return false; }
        }
    }

    public class SingleObjectUnavailable<T> : ISingleObject<T>
    {
        public bool IsAvailable
        {
            get { return false; }
        }
        public bool FoundMultiple
        {
            get { return false; }
        }
        public T Object
        {
            get { throw new Exception("Trying to access a none available object"); }
        }

    }

    public class SingleObjectList<T> : ISingleObject<T>
    {
        public bool IsAvailable
        {
            get { return false; }
        }

        public bool FoundMultiple
        {
            get { return true; }
        }

        public T Object
        {
            get { throw new Exception("Multiple objects found while only one should be expected."); }
        }

    }

    
}
