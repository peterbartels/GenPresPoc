using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ext.Direct.Mvc;
using GenPres.Data;
using GenPres.Data.Connections;

namespace GenPres.Controllers
{
    public class SettingsController : Controller
    {
        //
        // GET: /Settings/

        public ActionResult BuildDatabase()
        {
            SessionManager.Instance.InitSessionFactory(DatabaseConnection.DatabaseName.GenPres, true);
            SessionManager.Instance.InsertData();
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
