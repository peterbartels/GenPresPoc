using System;
using System.Web;
using System.Web.Mvc;
using GenPres.Assembler;
using GenPres.Data.Connections;
using NHibernate;
using NHibernate.Context;
using StructureMap;

namespace GenPres.Web.Environments
{
    [AttributeUsage(AttributeTargets.Method,
      AllowMultiple = false)]
    public class NHibernateSessionAttribute
      : ActionFilterAttribute
    {

        protected ISessionFactory SessionFactory
        {
            get
            {
                return MvcApplication.GetSessionFactory(GetEnvironment());
            }
        }

        private static DatabaseConnection.DatabaseName GetEnvironment()
        {
            var environment = (DatabaseConnection.DatabaseName?)HttpContext.Current.Session["environment"];
            return environment ?? DatabaseConnection.DatabaseName.GenPres;
        }

        public override void OnActionExecuting(
          ActionExecutingContext filterContext)
        {
            ObjectFactory.Configure(x => x.For<ISessionFactory>().HttpContextScoped().Use(GenPresApplication.GetSessionFactory(GetEnvironment())));
            //var sessionFactory = SessionManager.Instance.InitSessionFactory(DatabaseConnection.DatabaseName.GenPres, false);
            var session = SessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
        }

        public override void OnActionExecuted(
          ActionExecutedContext filterContext)
        {
            try
            {
                var session = CurrentSessionContext.Unbind(SessionFactory);
                session.Close();

            }
            // ReSharper disable EmptyGeneralCatchClause
            catch (Exception)
            // ReSharper restore EmptyGeneralCatchClause
            {
                // ToDo: dirty hack, have to fix this
            }
        }

    }
}
