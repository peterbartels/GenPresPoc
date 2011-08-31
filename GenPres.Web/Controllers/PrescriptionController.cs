using System.Web.Mvc;
using Ext.Direct.Mvc;
using GenPres.Business.Service;
using GenPres.Data.DTO.Prescriptions;
using GenPres.Service;
using GenPres.Web.Controllers;

namespace GenPres.Controllers
{
    public class PrescriptionController : BaseController
    {
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
        public ActionResult GetSubstanceUnits(string generic, string shape, string route)
        {
            return this.Direct(PrescriptionService.GetSubstanceUnits(generic, shape, route));
        }
        public ActionResult GetComponengtUnits(string generic, string shape, string route)
        {
            return this.Direct(PrescriptionService.GetComponentUnits(generic, shape, route));
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
        public ActionResult UpdatePrescription(string patientId, PrescriptionDto prescriptionDto)
        {
            return this.Direct(PrescriptionService.UpdatePrescription(prescriptionDto, patientId));
        }
        public ActionResult ClearPrescription()
        {
            return this.Direct(PrescriptionService.ClearPrescription());
        }
    }
}
