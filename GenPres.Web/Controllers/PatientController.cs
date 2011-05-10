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
    public class PatientController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            Settings.SettingsManager.Instance.Initialize();
            PatientAssembler.RegisterDependencies();
        }

        public ActionResult GetLogicalUnits()
        {
            return this.Direct(PatientService.GetLogicalUnits());
        }

        public ActionResult GetPatientsByLogicalUnit(object node)
        {
            return this.Direct(PatientService.GetPatientsByLogicalUnit(1));
        }
    }
}
