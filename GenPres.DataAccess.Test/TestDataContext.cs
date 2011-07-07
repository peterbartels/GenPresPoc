using System;
using System.Data.Linq;

namespace GenPres.DataAccess.Test
{
    public class TestDataContextFactory : IDataContextFactory
    {
        private DataContext _dataContext;
        private bool _disposed = false;

        public TestDataContextFactory()
        {
            CreateTestDataContext();
        }

        public TestDataContext GetTestDataContext()
        {
            if(_disposed) CreateTestDataContext();
            return (TestDataContext) _dataContext;
        }

        public void CreateTestDataContext()
        {
            _dataContext = new TestDataContext(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Development\GenPres-Development\GenPres\GenPres.DataAccess.Test\TestDatabase.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
            _dataContext.Connection.Disposed += onDispose;
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
