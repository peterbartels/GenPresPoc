using System.Web.Mvc;
using Ext.Direct.Mvc;
using Informedica.GenPres.Assembler;
using Informedica.GenPres.Service;
using Informedica.GenPres.Web.Environments;

namespace Informedica.GenPres.Web.Controllers
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
