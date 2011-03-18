using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ext.Direct.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;


namespace GenPres.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //CacheManager cacheManager;

            //cacheManager = (CacheManager)CacheFactory.GetCacheManager();
            //cacheManager.Add(Guid.NewGuid().ToString(), 1);
            return View();
        }

        public ActionResult Settings()
        {
            return View();
        }
    }
}
