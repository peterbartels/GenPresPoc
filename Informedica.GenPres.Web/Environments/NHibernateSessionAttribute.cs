using System;
using System.Web;
using System.Web.Mvc;
using Informedica.GenPres.Assembler;
using Informedica.GenPres.Data;
using Informedica.GenPres.Data.Connections;
using NHibernate;
using NHibernate.Context;
using StructureMap;

namespace Informedica.GenPres.Web.Environments
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
                return MvcApplication.GetSessionFactory(DatabaseConnection.DatabaseName.GenPresTest);
            }
        }


        public override void OnActionExecuting(
          ActionExecutingContext filterContext)
        {
            var sessionFactory = GenPresApplication.GetSessionFactory(DatabaseConnection.DatabaseName.GenPresTest);
            ObjectFactory.Configure(x => x.For<ISessionFactory>().HttpContextScoped().Use(sessionFactory));
            TestSessionManager.Init();
            //var sessionFactory = SessionManager.Instance.InitSessionFactory(DatabaseConnection.Databa =Name.GenPres, false);
            //replaced for test, var session = SessionFactory.OpenSession();
            //replaced for test, CurrentSessionContext.Bind(session);
        }

        public override void OnActionExecuted(
          ActionExecutedContext filterContext)
        {
            try
            {
                //replaced for test, var session = CurrentSessionContext.Unbind(SessionFactory);
                //replaced for test, session.Close();
                GenPresApplication.CloseSessionFactory();
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
