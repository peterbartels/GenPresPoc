using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace GenPres.Business.Data
{
    public class DataAccessManager : IDisposable
    {
        private DataContext _dataContext;
        private DataAccessCache _cache;
        private DataAccessMapper _mapper;
        //private IDataService _service;

        public DataAccessManager(DataContext context)
        {
            _dataContext = context;
            DataAccessGateway.Instance.DataAccessManager = this;
            _cache = new DataAccessCache(_dataContext);
            _mapper = new DataAccessMapper();
        }

        public DataAccessMapper Mapper {
            get
            {
                return _mapper;
            }
        }

        public DataAccessCache Cache
        {
            get
            {
                return _cache;
            }
        }


        public DataContext DataContext
        {
            get
            {
                return _dataContext;
            }
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}


