using System.Web.Mvc;

namespace INMETRO.CIPP.API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }
       
    }
}