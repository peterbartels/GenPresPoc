using System;
using GenPres.Database;
using System.Data.Linq;

namespace GenPres.Data.Managers
{
    public class GenPresDataContextManager : IDataContextManager
    {
        private DataContext _dataContext;
        private bool _disposed;

        public GenPresDataContextManager()
        {
            CreateGenPresDataContext();
        }

        public PrescriptionDataContext GetGenPresDataContext()
        {
            if(_disposed) CreateGenPresDataContext();
            return (PrescriptionDataContext)_dataContext;
        }

        public void CreateGenPresDataContext()
        {
            _dataContext = new PrescriptionDataContext(DatabaseConnection.GetConnectionString(DatabaseConnection.DatabaseName.GENPRES));
            _dataContext.Connection.Disposed += onDispose;
            _disposed = false;
        }

        public DataContext Context
        {
            get
            {
                if (_disposed) CreateGenPresDataContext();
                return _dataContext;
            }
        }
        public void Submit()
        {
            using (var ctx = GetGenPresDataContext())
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
