using System.Web.Mvc;
using Ext.Direct.Mvc;
using Informedica.GenPres.Service;
using Informedica.GenPres.Web.Environments;

namespace Informedica.GenPres.Web.Controllers
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
