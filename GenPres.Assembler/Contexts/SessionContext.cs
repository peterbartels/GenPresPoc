using System;
using GenPres.Data;
using NHibernate;
using NHibernate.Context;

namespace GenPres.Assembler.Contexts
{
    public class SessionContext : ICurrentSessionContext, IDisposable
    {
        public SessionContext()
        {
            CurrentSessionContext.Bind(SessionFactoryManager.Instance.SessionFactoryFromInstance.OpenSession());
        }

        public void Dispose()
        {
            var session = CurrentSessionContext.Unbind(SessionFactoryManager.Instance.SessionFactoryFromInstance);
            session.Close();
        }

        public ISession CurrentSession()
        {
            return SessionFactoryManager.Instance.SessionFactoryFromInstance.GetCurrentSession();
        }

        public static SessionContext UseContext()
        {
            return new SessionContext();
        }
    }
}