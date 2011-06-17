using System.Web.Mvc;
using Ext.Direct.Mvc;
using GenPres.Assembler;
using GenPres.Business.Service;

namespace GenPres.Controllers
{
    public class PrescriptionController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            Settings.SettingsManager.Instance.Initialize(HttpContext.ApplicationInstance.Server.MapPath("~/"));
            GenPresApplication.Initialize();
        }

        public ActionResult GetGenerics()
        {
            return this.Direct(MedicineService.GetGenerics());
        }

        public ActionResult GetRoutes()
        {
            return this.Direct(MedicineService.GetRoutes());
        }
        public ActionResult GetShapes()
        {
            return this.Direct(MedicineService.GetShapes());
        }

    }
}
