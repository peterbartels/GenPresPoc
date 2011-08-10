using System;
using System.Data.Linq;
using GenPres.Data.Managers;
using GenPres.Database;

namespace GenPres.xTest.Base
{
    public class TestDataContextManager : IDataContextManager
    {
        private DataContext _dataContext;
        private bool _disposed = false;

        public TestDataContextManager()
        {
            CreateTestDataContext();
        }

        public PrescriptionDataContext GetTestDataContext()
        {
            if(_disposed)
            {
                CreateTestDataContext();
                _disposed = false;
            }
            return (PrescriptionDataContext)_dataContext;
        }

        public void CreateTestDataContext()
        {
            _dataContext = new PrescriptionDataContext(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Development\GenPres\GenPres.xTest.Data\GenPres.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
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
