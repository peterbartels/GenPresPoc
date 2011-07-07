using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Database;
using DB=GenPres.Database;
using System.Data.Linq;

namespace GenPres.DataAccess
{
    public class GenPresDataContext : IDataContextFactory
    {
        private readonly PrescriptionDataContext _dataContext;

        public  GenPresDataContext()
        {
            _dataContext = new PrescriptionDataContext(DatabaseConnection.GetConnectionString(DatabaseConnection.DatabaseName.GENPRES));
            
        }

        public DataContext Context
        {
            get
            {
                return _dataContext;
            }
        }
        public void SaveAll()
        {
            
        }
    }
}
