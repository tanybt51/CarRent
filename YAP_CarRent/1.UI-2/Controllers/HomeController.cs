using _1.UI.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SessionManager.LastPage = new List<string>() { "Index", "Home" };
            
           ViewBag.user = SessionManager.User;
            return View();
        }
       
        public ActionResult About()
        {
            SessionManager.LastPage = new List<string>() { "About", "Home" };
            ViewBag.Message = "עלינו לשבח";
            ViewBag.user = SessionManager.User;

            return View();
        }

        public ActionResult Contact()
        {
            SessionManager.LastPage = new List<string>() { "Contact", "Home" };
            ViewBag.Message = "Your contact page.";
            ViewBag.user = SessionManager.User;

            return View();
        }
        public ActionResult ContactMessage(string name, string email, string phone, string message)
        {
            // Do something with the message???
            return RedirectToAction("Index", "Home");
        }
    }
}