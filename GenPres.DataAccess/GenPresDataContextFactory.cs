using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Database;
using DB=GenPres.Database;
using System.Data.Linq;

namespace GenPres.DataAccess
{
    public class GenPresDataContextFactory : IDataContextFactory
    {
        private DataContext _dataContext;
        private bool _disposed;

        public GenPresDataContextFactory()
        {
            CreateTestDataContext();
        }

        public PrescriptionDataContext GetTestDataContext()
        {
            if(_disposed) CreateTestDataContext();
            return (PrescriptionDataContext)_dataContext;
        }

        public void CreateTestDataContext()
        {
            _dataContext = new PrescriptionDataContext(DatabaseConnection.GetConnectionString(DatabaseConnection.DatabaseName.GENPRES));
            _dataContext.Connection.Disposed += onDispose;
            _disposed = false;
        }

        public DataContext Context
        {
            get
            {
                if (_disposed) CreateTestDataContext();
                return _dataContext;
            }
        }
        public void SaveAll()
        {
            using (var ctx = GetTestDataContext())
            {
                ctx.SubmitChanges();
            }
        }
        protected void onDispose(object sender, EventArgs e)
        {
            _disposed = true;
        }
    }
}
