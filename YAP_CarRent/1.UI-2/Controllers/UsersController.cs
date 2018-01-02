using _1.UI_2.Models;
using _2.BL;
using _4.Entities;
using _4.Entities.Validation;
using _4.Entities.ExtentionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1.UI_2.Controllers
{
    [Authorize(Roles = "Manager,Admin")]
    public class UsersController : Controller
    {
        UserManager manager;
        public UsersController()
        {
            manager = new UserManager();
        }
        // GET: Users
        public ActionResult Index()
        {
            return View("Manage");
        }

        public ActionResult Create()
        {
            var vm = InitEditUserVM(new User());
            return View(vm);
        }

        public ActionResult Create(User _user)
        {
            if (Request.IsAuthenticated)
            {
                _user.Roles = SetNewRoles(_user);
                if (manager.RegisterNewUser(_user))
                    return View("Index");
                else
                {
                    return View();
                }
            }
            return View();
        }

        public ActionResult Manage()
        {
            // ManageUsersVM mangaeVM = new ManageUsersVM(manager.Users, manager.Roles, 10);
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var user = manager.Users.Where(u => u.ID == ID).FirstOrDefault();
            var editUserVM = InitEditUserVM(user);
            return View(editUserVM);
        }
        [HttpPost]
        public ActionResult Edit(User _user)
        {
            if (Request.IsAuthenticated)
            {
                bool b = manager.EditUser(_user);
                bool b1 = manager.AddRolesToUser(_user, SetNewRoles(_user));
                manager.Dispose();
                if (b && b1)
                    return RedirectToAction("Manage");
                else
                    return Edit(_user.ID);
            }
            else
            {

                var editUserVM = InitEditUserVM(_user);
                return View(editUserVM);
            }
        }

        public ActionResult InitPage(int perPage)
        {
            var users = manager.Users.Take(perPage).ToList();

            var initPage = new ManageUsersVM(users, manager.Roles, perPage);
            return Json(initPage, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ToPage(int perPage, int from)
        {


            return Json(manager.Users.Skip(from).Take(perPage).ToList(), JsonRequestBehavior.AllowGet);
        }

        private EditUserVM InitEditUserVM(User _user)
        {


            var _userroles = _user.Roles.ToList();
            var _roles2 = manager.Roles.Where(r => !_userroles.Contains(r)).ToList();
            EditUserVM editUserVM = new EditUserVM() { User = _user, Roles2 = _roles2 };
            return editUserVM;
        }


        public JsonResult TZ(int tz)
        {

            return Json(new TZAttribute().IsValid(tz), JsonRequestBehavior.AllowGet);


        }

        private List<Role> SetNewRoles(User _user)
        {
            List<User> _users = new List<_4.Entities.User>() { _user };
            var userRoles = _user.UserRoles.Split(',');
            List<Role> newRoles = manager.Roles.Where(r => userRoles.Contains(r.Name)).Select(r => new Role() { ID = r.ID, Name = r.Name, Users = _users }).ToList();
            //_user.Roles = newRoles;
            return newRoles;
        }

    }

}