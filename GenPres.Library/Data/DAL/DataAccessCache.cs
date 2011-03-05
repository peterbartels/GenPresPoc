using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using System.Data.Linq;

namespace GenPres.Business.Data
{
    public class DataAccessCache
    {
        private DataContext _dataContext;

        public DataAccessCache(DataContext context) {
            _dataContext = context;
        }

        Dictionary<string, List<DataKeyMap>> _dataAccessKeys = new Dictionary<string, List<DataKeyMap>>();

        public void AddKey(Type type, Expression sourceMember, Expression destinationMember)
        {
            DataKeyMap keyMap = DataKeyMap.GetKeyMap(type, sourceMember, destinationMember, _dataContext);
            if(!_dataAccessKeys.ContainsKey(type.Name)){
                _dataAccessKeys[type.Name] = new List<DataKeyMap>();
            }

            DataKeyMap exists = _dataAccessKeys[type.Name].Where(
                x => x.DataKeyProperty == keyMap.DataKeyProperty &&
                x.ObjectKeyProperty == keyMap.ObjectKeyProperty
            ).SingleOrDefault();
            if (exists != null)
            {
                _dataAccessKeys[type.Name].Remove(exists);
            }
            _dataAccessKeys[type.Name].Add(keyMap);
        }

        public List<DataKeyMap> GetKeys(string type)
        {
            if (_dataAccessKeys.Keys.Contains<string>(type))
            {
                return _dataAccessKeys[type];
            }
            return null;
        }
    }
}
