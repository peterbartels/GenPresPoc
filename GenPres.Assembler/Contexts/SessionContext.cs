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
            CurrentSessionContext.Bind(SessionManager.Instance.SessionFactoryFromInstance.OpenSession());
        }

        public void Dispose()
        {
            var session = CurrentSessionContext.Unbind(SessionManager.Instance.SessionFactoryFromInstance);
            session.Close();
        }

        public ISession CurrentSession()
        {
            return SessionManager.Instance.SessionFactoryFromInstance.GetCurrentSession();
        }

        public static SessionContext UseContext()
        {
            return new SessionContext();
        }
    }
}