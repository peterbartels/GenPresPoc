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
            if(_disposed)
            {
                CreateTestDataContext();
                _disposed = false;
            }
            return (TestDataContext) _dataContext;
        }

        public void CreateTestDataContext()
        {
            _dataContext = new TestDataContext(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Development\GenPres-Development\GenPres\GenPres.DataAccess.Test\GenPres.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
            _dataContext.Connection.Disposed += OnDispose;
        }

        public DataContext Context
        {
            get
            {
                if (_disposed)
                {
                    CreateTestDataContext();
                    _disposed = false;
                }
                return _dataContext;
            }
        }
        public void Submit()
        {
            using (var ctx = GetTestDataContext())
            {
                ctx.SubmitChanges();
            }
        }
        protected void OnDispose(object sender, EventArgs e)
        {
            _disposed = true;
        }
    }
}
