using System;
using System.Data.Linq;

namespace GenPres.DataAccess.Managers
{
    public class DataManager<C> : IDisposable where C : DataContext
    {
        private Csla.Data.ContextManager<C> _ctxMan;
        private static object _lock = new object();

        public DataManager(string connectionString)
        {
            _ctxMan = Csla.Data.ContextManager<C>.GetManager(connectionString, false);
        }

        public C GetContext()
        {
            return (C)_ctxMan.DataContext;
        }
        public void SubmitChanges()
        {
            _ctxMan.DataContext.SubmitChanges();
        }
        public void Dispose()
        {
            _ctxMan.Dispose();
        }
    }
}
