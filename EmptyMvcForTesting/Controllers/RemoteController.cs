using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Needletail.Mvc;
using Needletail.Mvc.Communications;
using EmptyMvcForTesting.Utils;

namespace EmptyMvcForTesting.Controllers
{
    public class RemoteController : RemoteExecutionController
    {

        public RemoteController()
        {
            this.IncommingConnectionIdAssigned += new IncommingConnectionIdAssignedDelegate(ApiTestController_IncommingConnectionIdAssigned);
            this.IncommingConnectionAssigningId += new IncommingConnectionAssigningIdDelegate(ApiTestController_IncommingConnectionAssigningId);
            RemoteExecutionController.ConnectionLost += new ConnectionLostDelegate(TwoWayController_ConnectionLost);
            
        }


        void TwoWayController_ConnectionLost(ClientCall call)
        {
            //remove it from the logged users
            UserHelper.UserLogoff(call.ClientId);
            //tell everybody to reload the userlist
            ReloadUserList();
        }

        string ApiTestController_IncommingConnectionAssigningId()
        {
            return User.Identity.IsAuthenticated ? User.Identity.Name : Guid.NewGuid().ToString();
        }

        void ApiTestController_IncommingConnectionIdAssigned(string newId)
        {
            //code that needs to run after a connection has been succesfully configured
        }

        public static void ReloadUserList()
        {
            dynamic reload = new ClientCall { CallerId = string.Empty, ClientId = string.Empty };
            reload.getUsers();
            RemoteExecution.BroadcastExecuteOnClient(reload);
        }		       
        
    }
}
