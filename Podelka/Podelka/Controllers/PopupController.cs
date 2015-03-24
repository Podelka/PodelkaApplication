using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Podelka.Controllers
{
    public class PopupController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(String d)
        {
            return RedirectToAction("Par", "Popup");
        }
        [HttpGet]
        public ActionResult Par()
        {
            return PartialView("~/Views/Account/_RegisterConfirmation.cshtml");
        }
    }
}