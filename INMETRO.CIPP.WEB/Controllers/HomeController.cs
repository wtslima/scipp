﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INMETRO.DIOIS.INSPECAO.WEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Login");

            return View();
        }
       
    }
}