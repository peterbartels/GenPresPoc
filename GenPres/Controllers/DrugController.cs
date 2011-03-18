using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Ext.Direct.Mvc;
using GenPres.PrescriptionCalculator;
using Csla;
using Newtonsoft.Json.Linq;

namespace GenPres.Controllers
{
    public class DrugController : Controller
    {
        //
        // GET: /Drug/
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            Settings.SettingsManager.Instance.SetSettingsPath(HttpContext.ApplicationInstance.Server.MapPath("~/"));
        }

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Get all possible routes
        /// </summary>
        /// <param name="paramvalues"></param>
        /// <returns></returns>
        public ActionResult GetRoutes(Newtonsoft.Json.Linq.JObject paramvalues)
        {
            GenForm.Products.Products ProductsWS = new GenForm.Products.Products();
            string generic = paramvalues.Value<string>("generic");
            string shape = paramvalues.Value<string>("shape");
            string unitgroup = paramvalues.Value<string>("unitgroup");

            GenForm.Products.DrugData drugData = ProductsWS.GetGenPresDrugData(generic, "", "", "", "", "", shape, "");
            string[] routes = drugData.Routes;
            Hashtable[] RoutesResult = new Hashtable[routes.Length];
            for (int i = 0; i < routes.Length; i++)
            {
                Hashtable obj = new Hashtable();
                obj["Id"] = i;
                obj["Name"] = routes[i];
                RoutesResult[i] = obj;
            }
            return this.Direct(RoutesResult);
        }

        /// <summary>
        /// Get all possible solutions
        /// </summary>
        /// <param name="paramvalues"></param>
        /// <returns></returns>
        public ActionResult GetSolutions(Newtonsoft.Json.Linq.JObject paramvalues)
        {
            Hashtable[] result = new Hashtable[3];
            result[0] = new Hashtable();
            result[0]["Id"] = "0";
            result[0]["Name"] = "FZ";
            result[1] = new Hashtable();
            result[1]["Id"] = "1";
            result[1]["Name"] = "Glucose 5%";
            result[2] = new Hashtable();
            result[2]["Id"] = "2";
            result[2]["Name"] = "Glucose 10%";
            return this.Direct(result);
        }

        /// <summary>
        /// Get all units for dosage type fields
        /// </summary>
        /// <param name="paramvalues"></param>
        /// <returns></returns>
        public ActionResult GetDosageUnits(Newtonsoft.Json.Linq.JObject paramvalues)
        {
            GenForm.Products.Products ProductsWS = new GenForm.Products.Products();
            string generic = paramvalues.Value<string>("generic");
            string shape = paramvalues.Value<string>("shape");
            string route = paramvalues.Value<string>("route");
            string unit = paramvalues.Value<string>("unit");

            GenForm.Products.DrugData presUnits = ProductsWS.GetGenPresDrugData(generic, "", "", "", "", "", shape, route);

            Hashtable[] UnitResult = new Hashtable[presUnits.SubstanceUnits.Length];
            for (int i = 0; i < presUnits.SubstanceUnits.Length; i++)
            {
                Hashtable obj = new Hashtable();
                obj["Id"] = i;
                obj["Name"] = presUnits.SubstanceUnits[i].Trim();
                UnitResult[i] = obj;
            }

            if (unit == "mg" || unit == "microg" || unit == "g" || (UnitResult.Length == 1 && ((string)UnitResult[0]["Name"] == "g" || (string)UnitResult[0]["Name"] == "mg" || (string)UnitResult[0]["Name"] == "microg")))
            {
                UnitResult = new Hashtable[3];
                Hashtable obj = new Hashtable();
                obj["Id"] = 0;
                obj["Name"] = "g";
                UnitResult[0] = obj;

                Hashtable obj2 = new Hashtable();
                obj2["Id"] = 1;
                obj2["Name"] = "mg";
                UnitResult[1] = obj2;

                Hashtable obj3 = new Hashtable();
                obj3["Id"] = 2;
                obj3["Name"] = "microg";
                UnitResult[2] = obj3;
            }
            return this.Direct(UnitResult);
        }

        /// <summary>
        /// Get units for shape type fields
        /// </summary>
        /// <param name="paramvalues"></param>
        /// <returns></returns>
        public ActionResult GetShapeUnits(Newtonsoft.Json.Linq.JObject paramvalues)
        {
            GenForm.Products.Products ProductsWS = new GenForm.Products.Products();
            string generic = paramvalues.Value<string>("generic");
            string shape = paramvalues.Value<string>("shape");
            string route = paramvalues.Value<string>("route");
            string unit = paramvalues.Value<string>("unit");

            GenForm.Products.DrugData presUnits = ProductsWS.GetGenPresDrugData(generic, "", "", "", "", "", shape, route);

            Hashtable[] UnitResult = new Hashtable[presUnits.ComponentUnits.Length];

            for (int i = 0; i < presUnits.ComponentUnits.Length; i++)
            {
                Hashtable obj = new Hashtable();
                obj["Id"] = i;
                obj["Name"] = presUnits.ComponentUnits[i].Trim();
                UnitResult[i] = obj;
            }

            if (UnitResult.Length == 1 && ((string)UnitResult[0]["Name"] == "g" || (string)UnitResult[0]["Name"] == "mg"))
            {
                UnitResult = new Hashtable[3];
                Hashtable obj = new Hashtable();
                obj["Id"] = 0;
                obj["Name"] = "g";
                UnitResult[0] = obj;

                Hashtable obj2 = new Hashtable();
                obj2["Id"] = 1;
                obj2["Name"] = "mg";
                UnitResult[1] = obj2;

                Hashtable obj3 = new Hashtable();
                obj3["Id"] = 2;
                obj3["Name"] = "microg";
                UnitResult[2] = obj3;
            }

            return this.Direct(UnitResult);
        }

        /// <summary>
        /// Get generics from formulary
        /// </summary>
        /// <param name="paramvalues"></param>
        /// <returns></returns>
        public ActionResult GetGenerics(Newtonsoft.Json.Linq.JObject paramvalues)
        {
            GenForm.Products.Products ProductsWS = new GenForm.Products.Products();

            string shape = paramvalues.Value<string>("shape");
            string unitgroup = paramvalues.Value<string>("unitgroup");
            string route = paramvalues.Value<string>("route");

            GenForm.Products.DrugData drugData = ProductsWS.GetGenPresDrugData("", "", "", "", "", "", shape, route);
            string[] Generics = drugData.Substances;
            Hashtable[] GenericsResult = new Hashtable[Generics.Length];
            for (int i = 0; i < Generics.Length; i++)
            {
                Hashtable obj = new Hashtable();
                obj["Id"] = i;
                obj["Name"] = Generics[i];
                GenericsResult[i] = obj;
            }
            return this.Direct(GenericsResult);
        }

        /// <summary>
        /// Get smallest possible concentrated value
        /// </summary>
        /// <param name="paramvalues"></param>
        /// <returns></returns>
        public ActionResult GetConcentrationKAEs(Newtonsoft.Json.Linq.JObject paramvalues)
        {
            GenForm.Products.Products ProductsWS = new GenForm.Products.Products();

            string shape = paramvalues.Value<string>("shape");
            string generic = paramvalues.Value<string>("generic");
            string route = paramvalues.Value<string>("route");

            GenForm.Products.DrugData drugData = ProductsWS.GetGenPresDrugData(generic, "", "", "", "", "", shape, route);
            string[] KAEs = drugData.ConcentrationKAEs;
            Hashtable[] KAEsResult = new Hashtable[KAEs.Length];
            for (int i = 0; i < KAEs.Length; i++)
            {
                Hashtable obj = new Hashtable();
                obj["Id"] = i;
                obj["Name"] = KAEs[i];
                KAEsResult[i] = obj;
            }
            return this.Direct(KAEsResult);
        }


        /// <summary>
        /// Get all possible shapes from formulary
        /// </summary>
        /// <param name="paramvalues"></param>
        /// <returns></returns>
        public ActionResult GetShapes(Newtonsoft.Json.Linq.JObject paramvalues)
        {
            GenForm.Products.Products ProductsWS = new GenForm.Products.Products();

            string generic = paramvalues.Value<string>("generic");
            string unitgroup = paramvalues.Value<string>("unitgroup");
            string route = paramvalues.Value<string>("route");

            GenForm.Products.DrugData drugData = ProductsWS.GetGenPresDrugData(generic, "", "", "", "", "", "", route);
            string[] Shapes = drugData.Shapes;

            Hashtable[] ShapeResult = new Hashtable[Shapes.Length];
            for (int i = 0; i < Shapes.Length; i++)
            {
                Hashtable obj = new Hashtable();
                obj["Id"] = i;
                obj["Name"] = Shapes[i];
                ShapeResult[i] = obj;
            }
            return this.Direct(ShapeResult);
        }
    }
}
