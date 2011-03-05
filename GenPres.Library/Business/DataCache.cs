using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business
{
    public sealed class DataAccessCache
    {
        static DataAccessCache instance = null;
        static readonly object padlock = new object();
        
        private Dictionary<string, string> _associations = new Dictionary<string, string>();
        private Dictionary<string, object> _currentDataAccessObject = new Dictionary<string, object>();
        private Dictionary<string, object> _allDataAccessObject = new Dictionary<string, object>();
        private List<string> _collectionIterator = new List<string>();
        
        public DataAccessCache(){}

        public int GetCollectionIterator(string name)
        {
            int count = (from i in _collectionIterator where i == name select i).Count();
            _collectionIterator.Add(name);
            return count;
        }

        public object GetCurrentDataAccessObject(string name)
        {
            object result;
            _currentDataAccessObject.TryGetValue(name, out result);
            return result;
        }

        public void SetCurrentDataAccessObjectToAll()
        {
            _allDataAccessObject.Clear();
            foreach (KeyValuePair<string, object> pair in _currentDataAccessObject)
            {
                _allDataAccessObject.Add(pair.Key, pair.Value);
            }
            _currentDataAccessObject.Clear();
        }

        public void SetCurrentDataAccessObject(string name, object obj)
        {
            _currentDataAccessObject[name] = obj;
        }


        public object GetAllDataAccessObject(string name)
        {
            object result;
            _allDataAccessObject.TryGetValue(name, out result);
            return result;
        }


        public void SetAllDataAccessObject(string name, object obj)
        {
            _allDataAccessObject[name] = obj;
        }

        public void AddAssociation(string name1, string name2)
        {
            _associations[name1] = name2;
        }

        public bool HasAssociation(string name1, string name2)
        {
            return ((
                from i in _associations 
                where i.Key == name1 && i.Value == name2 || i.Key == name2 && i.Value == name1
                select i).Count() > 0);
        }

        public static DataAccessCache Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DataAccessCache();
                    }
                    return instance;
                }
            }
            set
            {
                instance = value;
            }
        }
    }
}

