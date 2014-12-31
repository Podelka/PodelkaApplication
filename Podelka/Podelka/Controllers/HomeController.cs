using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Podelka.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Rules()
        {
            ViewBag.Message = "Your rules page.";
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Cooperation()
        {
            ViewBag.Message = "Your Cooperation page.";
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Help()
        {
            ViewBag.Message = "Your help page.";
            return View();
        }
    }
}