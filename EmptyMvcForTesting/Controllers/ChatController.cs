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

    public class ChatController : Controller
    {

        public string CurrentUser
        {
            get 
            {
                return Request.Cookies["chatUser"].Value;
            }
        }
        //
        // GET: /Chat/

        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetUsers()
        {
            var online = new List<object>();
            foreach (var u in RemoteController.CurrentUsers) 
                if(u != CurrentUser)
                    online.Add(new { ClientId = u  });

            return Json(online);
        }

        
        public TwoWayResult SendMessageTo(string messageTo, string message)
        {
            dynamic call = new ClientCall { ClientId = messageTo, CallerId = CurrentUser };
            //make the remote call
            call.messageReceived(CurrentUser, message , true);
            return new TwoWayResult(call);
        }

    }
}
