using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Ext.Direct.Mvc;
using GenPres.Business;
using Csla;
using Newtonsoft.Json.Linq;
using GenPres.Business.Data;

namespace GenPres.Controllers
{
    
    public class PrescriptionController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            Settings.SettingsManager.Instance.SetSettingsPath(HttpContext.ApplicationInstance.Server.MapPath("~/"));
        }

        public ActionResult Index()
        {
            //Settings.SettingsManager.Instance
            return View();
        }
    }
}