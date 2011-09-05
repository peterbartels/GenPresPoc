using System.Collections.Generic;
using System.Web.Mvc;
using Ext.Direct.Mvc;
namespace Informedica.GenForm.Mvc3.Controllers
{
    public class TestsController : Controller
    {
        //
        // GET: /Tests/

        public ActionResult Index()
        {
            ViewBag.Title = "GenPres Tests";
            return View();
        }
        public ActionResult ValueField()
        {
            ViewBag.Title = "GenPres Tests";
            return View();
        }

        public ActionResult GetTestData(string contents)
        {
            return this.Direct(contents);
        }

        public ActionResult GetTestStoreData(string contents)
        {
            var res = new Dictionary<int, Dictionary<string, string>>();
            res[0] = new Dictionary<string, string>();
            res[0]["test"] = "contents";
            return this.Direct(res);
        }
    }
}
