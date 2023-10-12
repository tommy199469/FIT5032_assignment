using FIT5032_assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace FIT5032_assignment.Controllers
{
    public class ReactController : Controller
    {
        private static readonly IList<XrayType> xrayType;

        static ReactController()
        {
            xrayType = new List<XrayType>
            {
                new XrayType
                {
                   name = "Full Body",
                   referenceMaxPrice = 10000,
                   referenceMinPrice = 1000
                },
                  new XrayType
                {
                   name = "Half Body",
                   referenceMaxPrice = 5000,
                   referenceMinPrice = 500
                }

            };
        }

        [OutputCache(Location = OutputCacheLocation.None)]
        public ActionResult GetXrays()
        {
            return Json(xrayType, JsonRequestBehavior.AllowGet);
        }


        // GET: React
        public ActionResult Index()
        {
            return View();
        }
    }
}