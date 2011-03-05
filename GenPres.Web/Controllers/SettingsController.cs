using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ext.Direct.Mvc;

namespace GenPres.Controllers
{
    public class SettingsController : Controller
    {
        //
        // GET: /Settings/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult SetSetting(string computerName, string name, string value)
        {
            Settings.SettingsManager.Instance.SetSettingsPath(HttpContext.ApplicationInstance.Server.MapPath("~/"));
            Settings.SettingsManager.Instance.CreateSecureSetting(computerName, name, value);
            
            return this.Direct("");
        }

    }
}
