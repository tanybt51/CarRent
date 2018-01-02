using _1.UI.Code;
using _1.UI_2.Models;
using _2.BL;
using _4.Entities;
using _4.Entities.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace _3.SP_UI.Controllers
{
    public class LoginController : Controller
    {

        const string ERROR_LOGIN = "כתובת המייל או הסיסמה אינם נכונים";

        UserManager userManager;
        List<User> users;


        public LoginController()
        {
            userManager = new UserManager();
            users = userManager.Users;

        }

        // GET: Login

        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                //ViewBag.User = users[2];
                return View("Index", masterName: "_LayoutLogin");
            }
            else
                return RedirectToAction(SessionManager.LastPage[0], SessionManager.LastPage[1]);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginInfo loginInfo, string returnUrl)
        {
            var tempUser = users.Where(u => u.Email == loginInfo.Email && u.Password.Trim() == loginInfo.Password).FirstOrDefault();
            if (tempUser != null)
            {
                /* login succeeded !!! :) */
                LoginUser(tempUser);

                //return RedirectToAction(SessionManager.LastPage[0], SessionManager.LastPage[1]);
                if (string.IsNullOrEmpty(returnUrl))
                {
                    // redirect to the default page
                    //return RedirectToAction("Index","Home");
                    return RedirectToAction(SessionManager.LastPage[0], SessionManager.LastPage[1]);

                }
                else
                {
                    // redurect to the last requested page
                    return Redirect(returnUrl);
                }
            }
            else
                ViewBag.error = ERROR_LOGIN;
            return View("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginOld(LoginInfo loginInfo)
        {
            if (SessionManager.IsConnected)
            {

                var tempUser = users.Where(u => u.Email == loginInfo.Email && u.Password.Trim() == loginInfo.Password).FirstOrDefault();
                if (tempUser != null)
                {
                    SessionManager.User = tempUser;
                   var b= HttpContext.User.IsInRole("dddd");
                    ViewBag.b = b;
                }
                else
                {

                    ViewBag.error = ERROR_LOGIN;
                    ViewBag.email = loginInfo.Email;
                    return View("Index", masterName: "_LayoutLogin");
                }

            }
            else
            {
                SessionManager.LastPage = new List<string>() { "Index", "Home" };

            }
            // Redirect to the last page the user was
            return RedirectToAction(SessionManager.LastPage[0], SessionManager.LastPage[1]);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {

            FormsAuthentication.SignOut(); // remove the authentication cookie

            Session.Abandon(); // close the Session for the specific user



            return RedirectToAction(SessionManager.LastPage[0], SessionManager.LastPage[1]);

        }

        [ValidateAntiForgeryToken]
        public ActionResult Register()
        {




            return View("Register", masterName: "_LayoutLogin");


        }

        [ValidateAntiForgeryToken]
        public ActionResult RegisterNewUser(User user)
        {
            if (ModelState.IsValid)
            {
               
                userManager.RegisterNewUser(user);
                user.Roles.Add(userManager.Roles.Where(r => r.Name == "User").FirstOrDefault());
                userManager.AddRolesToUser(user, user.Roles.ToList());
                LoginUser(user);
                return RedirectToAction(SessionManager.LastPage[0], SessionManager.LastPage[1]);
            }
            else
            {
                string errors = "";
                foreach (var value in ModelState.Values)
                {
                    foreach (var er in value.Errors)
                    {
                        errors += errors + er.ErrorMessage + "<br/>";
                    }
                }
                ViewBag.errors = errors;
                return View("Register", masterName: "_LayoutLogin");
            }







        }


        //
        private bool IsPassword(User user, string password)
        {
            if (user.Password == password)
                return true;
            return false;
        }

        public JsonResult TZ(int tz)
        {

            return Json(new TZAttribute().IsValid(tz), JsonRequestBehavior.AllowGet);


        }

        public JsonResult Email(string _email)
        {
            var _tempUser = userManager.Users.Where(e => e.Email == _email).FirstOrDefault();

            return Json(_tempUser!=null, JsonRequestBehavior.AllowGet);


        }

        private void LoginUser(User _user)
        {
            _user.LastEntryDTime = DateTime.Now;
            // save the user identity in a cookie & session
            FormsAuthentication.SetAuthCookie(_user.Email, false);
            SessionManager.User = _user;
            userManager.UpdateLogin(_user);
        }
    }
}