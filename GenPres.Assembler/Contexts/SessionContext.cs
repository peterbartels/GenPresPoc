using System;
using NHibernate;
using NHibernate.Context;

namespace GenPres.Assembler.Contexts
{
    public class SessionContext : ICurrentSessionContext, IDisposable
    {
        public SessionContext()
        {
            CurrentSessionContext.Bind(GenPresApplication.Instance.SessionFactoryFromInstance.OpenSession());
        }

        public void Dispose()
        {
            var session = CurrentSessionContext.Unbind(GenPresApplication.Instance.SessionFactoryFromInstance);
            session.Close();
        }

        public ISession CurrentSession()
        {
            return GenPresApplication.Instance.SessionFactoryFromInstance.GetCurrentSession();
        }

        public static SessionContext UseContext()
        {
            return new SessionContext();
        }
    }
}