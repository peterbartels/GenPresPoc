using System.Web.Mvc;
using Ext.Direct.Mvc;
using GenPres.Assembler;
using GenPres.Business.Data.Client.PrescriptionData;
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

        public ActionResult GetGenerics(string route, string shape)
        {
            return this.Direct(MedicineService.GetGenerics(route, shape));
        }
        public ActionResult GetRoutes(string generic, string shape)
        {
            return this.Direct(MedicineService.GetRoutes(generic, shape));
        }
        public ActionResult GetShapes(string generic, string route)
        {
            return this.Direct(MedicineService.GetShapes(generic, route));
        }
        public ActionResult GetPrescriptions(string patientId)
        {
            return this.Direct(PrescriptionService.GetPrescriptions(patientId));
        }
        public ActionResult GetPrescriptionById(int id)
        {
            return this.Direct(PrescriptionService.GetPrescriptionById(id));
        }
        public ActionResult SavePrescription(string patientId, PrescriptionDto prescriptionDto)
        {
            return this.Direct(PrescriptionService.SavePrescription(prescriptionDto, patientId));
        }
    }
}
