using System.Web.Mvc;

namespace Informedica.GenPres.Web.Controllers
{
    public class AppTestController : Controller
    {
        //
        // GET: /AppTest/

        public ActionResult Index()
        {
            ViewBag.Title = "GenPres UseCase";
            return View();
        }

    }
}
