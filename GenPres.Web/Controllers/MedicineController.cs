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


namespace GenPres.Controllers
{
    public class MedicineController : Controller
    {
        //
        // GET: /Medicine/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CalculateMedicine(Medicine medicine)
        {
            Prescription p = Prescription.NewPrescription();
            p.Medicine = medicine;
            GenPres.Operations.CalculationProcess.Start(p, p.Medicine);
            return this.Direct(medicine);
        }
    }
}
