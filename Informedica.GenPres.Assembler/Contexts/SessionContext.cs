using System;
using Informedica.GenPres.Data;
using NHibernate;
using NHibernate.Context;

namespace Informedica.GenPres.Assembler.Contexts
{
    public class SessionContext : ICurrentSessionContext, IDisposable
    {
        public SessionContext()
        {
            CurrentSessionContext.Bind(SessionManager.SessionFactoryFromInstance.OpenSession());
        }

        public void Dispose()
        {
            var session = CurrentSessionContext.Unbind(SessionManager.SessionFactoryFromInstance);
            session.Close();
            session.Dispose();
        }

        public ISession CurrentSession()
        {
            return SessionManager.SessionFactoryFromInstance.GetCurrentSession();
        }

        public static SessionContext UseContext()
        {
            return new SessionContext();
        }
    }
}