using System.Web.Mvc;
using Ext.Direct.Mvc;
using GenPres.Assembler;
using GenPres.Business.Service;
using GenPres.Data;
using GenPres.Data.Connections;
using GenPres.Service;
using GenPres.Web.Controllers;

namespace GenPres.Controllers
{
    public class PatientController : BaseController
    {
        public ActionResult GetLogicalUnits()
        {
            return this.Direct(PatientService.GetLogicalUnits());
        }

        public ActionResult GetPatientsByLogicalUnit(string node, int logicalUnitId)
        {
            return this.Direct(PatientService.GetPatientsByLogicalUnit(logicalUnitId));
        }

        public ActionResult SelectPatient(string patientId)
        {
            return this.Direct(PatientService.SavePatient(patientId));
        }
    }
}
