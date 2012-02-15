using System.Web.Mvc;
using Ext.Direct.Mvc;
using Informedica.GenPres.Data;
using Informedica.GenPres.Data.Connections;

namespace Informedica.GenPres.Web.Controllers
{
    public class SettingsController : Controller
    {
        //
        // GET: /Settings/

        public ActionResult BuildDatabase()
        {
            SessionManager.InitSessionFactory(DatabaseConnection.DatabaseName.GenPres, true);
            SessionManager.InsertData();
            return View();
        }

        public ActionResult SetSetting(string computerName, string name, string value)
        {
            Settings.SettingsManager.Instance.Initialize(HttpContext.ApplicationInstance.Server.MapPath("~/"));
            Settings.SettingsManager.Instance.CreateSecureSetting(computerName, Settings.SettingsManager.DatabaseName, name, value);
            
            return this.Direct("");
        }

    }
}
