using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleForum.Controllers
{
    public class AjaxTestController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Test()
        {
            return View("Test");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CheckTestString(string teststr)
        {
            return Json(CheckTestStr(teststr));
            //return new JsonResult { Data = teststr.Length > 3 };
        }

        private bool CheckTestStr(string teststr)
        {
            return teststr != null && teststr.Length > 3;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Test(string teststr)
        {
            if (!CheckTestStr(teststr))
            {
                return View("TestResult", null, "ERROR!");
            }

            return View("TestResult", null, teststr);
        }

        public ActionResult Index()
        {
            
            return Test();
        }

    }
}
