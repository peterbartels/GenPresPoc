using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business.Data
{
    public class DataAccessGateway
    {
        private DataAccessManager _dataAccessManager;

        #region singleton
        static DataAccessGateway instance = null;
        static readonly object padlock = new object();

        public DataAccessGateway() { }

        public static DataAccessGateway Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DataAccessGateway();
                    }
                    return instance;
                }
            }
            set
            {
                instance = value;
            }
        }
        #endregion

        public DataAccessManager DataAccessManager
        {
            get
            {
                return _dataAccessManager;
            }
            set
            {
                _dataAccessManager = value;
            }
        }
    }
}
