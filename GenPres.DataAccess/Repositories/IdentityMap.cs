using System;
using System.Collections.Generic;

namespace GenPres.DataAccess.Repositories
{
    public class IdentityMap<TBo, TDao>
    {
        private readonly Dictionary<object, TDao> _dataObjects = new Dictionary<object, TDao>();

        public void Add(object id, TDao item)
        {
            _dataObjects.Add(id, item);
        }

        public TDao Find(object id)
        {
            if(_dataObjects.ContainsKey(id))
            {
                return _dataObjects[id];
            }
            throw new Exception("Find executed withoud checking if key exists.");
        }
        
        public bool ContainsKey(object id)
        {
            return _dataObjects.ContainsKey(id);
        }

        public void Clear()
        {
            _dataObjects.Clear();
        }

        public void Remove(object id)
        {
            _dataObjects.Remove(id);
        }
        public Type GetDaoType()
        {
            return typeof(TDao);
        }


    }
}
