using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ext.Direct.Mvc;
using GenPres.Assembler;
using GenPres.Business.Service;
using GenPres.Data;
using GenPres.Data.Connections;

namespace GenPres.Controllers
{
    public class UserController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GenPresApplication.Initialize();
            SessionManager.Instance.InitSessionFactory(DatabaseConnection.DatabaseName.GenPres);
            Settings.SettingsManager.Instance.Initialize(HttpContext.ApplicationInstance.Server.MapPath("~/"));
        }

        public ActionResult Login(string userName, string password)
        {
            GenPresApplication.Initialize();
            return this.Direct(new { success = UserService.AuthenticateUser(userName, password) });
        }
    }
}
