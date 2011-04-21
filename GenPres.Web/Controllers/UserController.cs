using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ext.Direct.Mvc;
using GenPres.Assembler;
using GenPres.Business.Service;

namespace GenPres.Controllers
{
    public class UserController : Controller
    {

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            Settings.SettingsManager.Instance.SetSettingsPath(@"C:\Users\vaio\Documents\GenPres_Refactored\GenPres\GenPres.Web\");
        }

        public ActionResult Login(string userName, string password)
        {
            UserAssembler.RegisterDependencies();
            return this.Direct(new { success = UserService.AuthenticateUser(userName, password) });
        }
    }
}
