using System.Web.Mvc;
using Ext.Direct.Mvc;
using GenPres.Service;
using GenPres.Web.Controllers;
using GenPres.Web.Environments;

namespace GenPres.Controllers
{
    public class PatientController : BaseController
    {
        [Transaction]
        public ActionResult GetLogicalUnits()
        {
            return this.Direct(PatientService.GetLogicalUnits());
        }

        [Transaction]
        public ActionResult GetPatientsByLogicalUnit(string node, int logicalUnitId)
        {
            return this.Direct(PatientService.GetPatientsByLogicalUnit(logicalUnitId));
        }

        [Transaction]
        public ActionResult SelectPatient(string patientId)
        {
            return this.Direct(PatientService.SavePatient(patientId));
        }
    }
}
