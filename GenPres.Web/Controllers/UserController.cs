using System.Web.Mvc;
using Ext.Direct.Mvc;
using GenPres.Assembler;
using GenPres.Business.Service;
using GenPres.Web.Controllers;
using GenPres.Web.Environments;

namespace GenPres.Controllers
{
    public class UserController : BaseController
    {
        [Transaction]
        public ActionResult Login(string userName, string password)
        {
            GenPresApplication.Initialize();
            return this.Direct(new { success = UserService.AuthenticateUser(userName, password) });
        }
    }
}
