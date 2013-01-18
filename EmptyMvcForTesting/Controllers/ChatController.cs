using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Needletail.Mvc;
using System.Web.Security;
using System.Dynamic;
using WebMatrix.WebData;
using EmptyMvcForTesting.Utils;

namespace EmptyMvcForTesting.Controllers
{

    [Authorize]
    public class ChatController : Controller
    {
        //
        // GET: /Chat/

        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetUsers()
        {
            var m = new EFModel.Entities();

            var usrs = m.UserProfiles.ToList();
            var online = new List<object>();

            foreach (var u in usrs) 
                if(u.UserName != User.Identity.Name && UserHelper.IsUserLogedIn(u.UserName))
                    online.Add(new { ClientId = u.UserName });

            return Json(online);
        }

        
        public TwoWayResult SendMessageTo(string messageTo, string message)
        {
            dynamic call = new ClientCall { ClientId = messageTo, CallerId = User.Identity.Name };
            //make the remote call
            call.messageReceived(User.Identity.Name, message , true);
            return new TwoWayResult(call);
        }

    }
}
