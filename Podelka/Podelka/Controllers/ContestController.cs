﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Podelka.Controllers
{
    public class ContestController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}