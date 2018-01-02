using _4.Entities;
using _1.UI_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Security.Principal;
using _2.BL;

namespace _1.UI.Code
{
    public static class SessionManager
    {
        const string USER = "User";
        const string LAST_PAGE = "LastPage";
        
        static SessionManager()
        {
            LastPage = new List<string>() { "Index", "Home" };
            User = new User()
            {
                Email = HttpContext.Current.User.Identity.Name,
             

            };
        }

        private static HttpSessionState Session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }

        public static User User
        {
            get
            {

                return Session[USER] as User;
            }
            set
            {
                Session[USER] = value;
            }
        }

        public static bool IsConnected
        {
            get
            {
                return Session[USER] != null;
            }

        }

        /// <summary>
        /// [0] = Action , [1] = Controller
        /// </summary>
        public static List<string> LastPage
        {
            get
            {
                if((Session[LAST_PAGE] as List<string>)[0]!=null)
                return Session[LAST_PAGE] as List<string>;
                else
                    return new List<string>() { "Index", "Home" };
            }
            set
            {
                Session[LAST_PAGE] = value;
            }
        }

        public static string MessageUserLastConnection
        {
            get
            {
                string name = User.FirstName;
                string lastEntry = ((DateTime)User.LastEntryDTime).ToString("HH:mm dd/MM/YY");
                return $"תאריך התחברות אחרון {lastEntry}";
            }

        }






    }
}