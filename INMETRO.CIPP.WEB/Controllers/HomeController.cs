using System;
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
            ViewBag.Title = "Home Page";
            return View();
        }

        //public ActionResult Login()
        //{
        //    ViewBag.Title = "Login Page";
        //    return View();
        //}

        //public ActionResult Download()
        //{
        //    ViewBag.Title = "Download Page";
        //    return View();
        //}
    }
}