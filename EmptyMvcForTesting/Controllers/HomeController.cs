using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Needletail.Mvc;

namespace EmptyMvcForTesting.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            ViewBag.TitlePage = "Chat app with SSE";
            return View();
        }

        [HttpPost]
        public ActionResult Index(string userName)
        {
            //add the cookie
            Response.Cookies.Add(new HttpCookie("chatUser", userName));
            RemoteController.ReloadUserList();
            //redirect to the chat page
            return RedirectToAction("Index", "Chat", new { });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}
