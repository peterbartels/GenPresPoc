using System.Web.Mvc;
using Ext.Direct.Mvc;
using GenPres.Business.Service;
using GenPres.Service;
using GenPres.Web.Controllers;
using GenPres.Web.Environments;

namespace GenPres.Controllers
{
    public class PrescriptionController : BaseController
    {
        [Transaction]
        public ActionResult GetGenerics(string route, string shape)
        {
            return this.Direct(MedicineService.GetGenerics(route, shape));
        }

        [Transaction]
        public ActionResult GetRoutes(string generic, string shape)
        {
            return this.Direct(MedicineService.GetRoutes(generic, shape));
        }

        [Transaction]
        public ActionResult GetShapes(string generic, string route)
        {
            return this.Direct(MedicineService.GetShapes(generic, route));
        }

        [Transaction]
        public ActionResult GetSubstanceUnits(string generic, string route, string shape)
        {
            return this.Direct(PrescriptionService.GetSubstanceUnits(generic, route, shape));
        }

        [Transaction]
        public ActionResult GetComponentUnits(string generic, string route, string shape)
        {
            return this.Direct(PrescriptionService.GetComponentUnits(generic, route, shape));
        }

        [Transaction]
        public ActionResult GetPrescriptions(string patientId)
        {
            return this.Direct(PrescriptionService.GetPrescriptions(patientId));
        }

        [Transaction]
        public ActionResult GetPrescriptionById(string id)
        {
            return this.Direct(PrescriptionService.GetPrescriptionById(id));
        }
        /*
        [Transaction]
        public ActionResult SavePrescription(string patientId, PrescriptionDto prescriptionDto)
        {
            return this.Direct(PrescriptionService.SavePrescription(prescriptionDto, patientId));
        }

        [Transaction]
        public ActionResult UpdatePrescription(string patientId, PrescriptionDto prescriptionDto)
        {
            return this.Direct(PrescriptionService.UpdatePrescription(prescriptionDto, patientId));
        }*/

        [Transaction]
        public ActionResult ClearPrescription()
        {
            return this.Direct(PrescriptionService.ClearPrescription());
        }
    }
}
