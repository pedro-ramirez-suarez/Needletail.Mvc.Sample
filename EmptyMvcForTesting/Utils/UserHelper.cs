using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmptyMvcForTesting.Models;
using EmptyMvcForTesting.Controllers;

namespace EmptyMvcForTesting.Utils
{
    public class UserHelper
    {
        public static List<string> Users = new List<string>();


        public static void UserLogedin(string userName)
        {
            if (!Users.Contains(userName))
                Users.Add(userName);
            RemoteController.ReloadUserList();
        }

        public static void UserLogoff(string userName)
        {
            if (Users.Contains(userName))
                Users.Remove(userName);
            RemoteController.ReloadUserList();
        }

        public static bool IsUserLogedIn(string userName)
        {
            return Users.Contains(userName);
        }
    }
}