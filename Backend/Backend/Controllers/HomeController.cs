﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Backend.Models;

namespace Backend.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return PartialView("AngularPartial");
        }
    }
}
