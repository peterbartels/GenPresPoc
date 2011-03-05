using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Ext.Direct.Mvc;
using System.Collections;
using GenPres.Business;
namespace GenPres.Controllers
{
    public class PatientController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            Settings.SettingsManager.Instance.SetSettingsPath(HttpContext.ApplicationInstance.Server.MapPath("~/"));
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetUnits(string nodeId)
        {
            //arraylist can be used as well
            DataRetriever dataRetreiver = new DataRetriever();
            List<LogicalUnit> units = dataRetreiver.GetUnits();
            List<Hashtable> TreeResults = new List<Hashtable>();
            if (nodeId == "root")
            {
                for (int i = 0; i < units.Count; i++)
                {
                    Hashtable TreeLeaf = new Hashtable();
                    TreeLeaf["leaf"] = false;
                    TreeLeaf["text"] = units[i].Name;
                    TreeLeaf["id"] = units[i].Id;
                    TreeResults.Add(TreeLeaf);
                }
            }
            else
            {
                List<Patient> patients = dataRetreiver.GetPatientsByLogicaUnit(nodeId);
                for (int i = 0; i < patients.Count; i++)
                {
                    Hashtable TreeLeaf = new Hashtable();
                    TreeLeaf["leaf"] = true;
                    TreeLeaf["text"] = patients[i].LastName + ", " + patients[i].FirstName;
                    TreeLeaf["id"] = patients[i].id;
                    TreeLeaf["FirstName"] = patients[i].FirstName;
                    TreeLeaf["Unit"] = patients[i].Unit;
                    TreeLeaf["PID"] = patients[i].PID;
                    TreeLeaf["Bed"] = patients[i].Bed;
                    TreeLeaf["LastName"] = patients[i].LastName;
                    TreeLeaf["Birthdate"] = "16-03-1983";
                    TreeLeaf["Age"] = "27";
                    TreeLeaf["AddmissionDate"] = patients[i].RegisterDate;
                    TreeLeaf["CurrentDate"] = DateTime.Now.ToString("dd-MM-yyyy");
                    TreeLeaf["Days"] = patients[i].DaysRegistered;
                    TreeLeaf["WeightGuess"] = "75";

                    TreeLeaf["Weight"] = patients[i].Weight;
                    TreeLeaf["Length"] = patients[i].Length;

                    TreeLeaf["WeightMedication"] = "75";
                    TreeLeaf["WeightActual"] = "75";
                    TreeLeaf["WeightAddmission"] = "75";
                    TreeLeaf["WeightBirth"] = "2";
                    TreeLeaf["LengthGuessed"] = "185";
                    TreeLeaf["LengthActual"] = "183";
                    
                    TreeResults.Add(TreeLeaf);
                }
            }            
            return this.Direct(TreeResults);
        }
    }
}
