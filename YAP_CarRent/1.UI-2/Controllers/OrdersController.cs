using _1.UI.Code;
using _1.UI_2.Models;
using _2.BL;
using _4.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1.UI_2.Controllers
{
    [Authorize(Roles = "User,Employee,Manager")]
    public class OrdersController : Controller
    {
        CarManager carManager;
        OrderManager orderManager;
        BranchManager branchManager;
        public OrdersController()
        {
            orderManager = new OrderManager();
            carManager = new CarManager();
        }
        // GET: Orders

        public ActionResult Index()
        {
            //var b1 = User.IsInRole("Employee,Manager");
            //var b2 = User.IsInRole("User");
            if (!User.IsInRole("Employee"))
            {
                ViewBag.User = GetUserFromSession();
            }
            //var b3 = User.IsInRole("Employee");

            return View();
        }

        public ActionResult GetAllUsers()
        {

            List<User> users;
            if (User.IsInRole("Employee"))
            {
                users = orderManager.GetAllUser();
            }
            else
            {
                users = new List<_4.Entities.User>() { SessionManager.User };
            }
            return Json(users, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Return(int id)
        {

            Order order = orderManager.Orders.Where(o => o.ID == id).FirstOrDefault();
            Model model = carManager.Models.Where(m => m.ID == order.Car.Model).FirstOrDefault();
            OrderVM vm = new OrderVM()
            {
                User = order.User1.FirstName,
                Car = order.Car.Serial,
                Branch = new BranchManager().Branches.Where(b => b.ID == order.Branch).Select(b => b.Name).FirstOrDefault(),
                Image = order.Car.Image,
                From = order.StartDate.ToString("dd/MM/yy"),
                To = order.EndDate.ToString("dd/MM/yy"),
                Return = DateTime.Now.ToString("dd/MM/yy"),
                Price = model.Group.PerDay * (order.EndDate - order.StartDate).Days,
                Fine = model.Group.DayFine * (DateTime.Now - order.EndDate).Days,
                ID = id
                
            };
            return View(vm);
        }

        [Authorize(Roles = "Employee")]
        [ValidateAntiForgeryToken]
        public ActionResult CloseOrder(int id, string _returnDate)
        {
            if (Request.IsAuthenticated)
            {
                DateTime returnDate = DateTime.Parse(_returnDate.Split('/')[1] + "/" + _returnDate.Split('/')[0] + "/" + _returnDate.Split('/')[2]);
                var order = orderManager.Orders.Where(o => o.ID == id).FirstOrDefault();
                order.ReturnDate = returnDate;
                bool b = orderManager.CloseOreder(order);

                if (b)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Return", id);
                }
            }
            else
                return RedirectToAction("Index");
            
        }

        public ActionResult Order(int _carNumber)
        {
            var car = carManager.Cars.Where(c => c.Serial == _carNumber).FirstOrDefault();
            OrderVM vm = new OrderVM()
            {
                Car = car.Serial,
                Branch = new BranchManager().Branches.Where(b => b.ID == car.Branch).Select(b => b.Name).FirstOrDefault(),
                Image = car.Image,
                Price = carManager.Models.Where(m => m.ID == car.Model).Select(p => p.Group.PerDay).FirstOrDefault()

            };

            return View(vm);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Create(string _from, string _to, int _carNumber, string _branch)
        {
            Order order = new Order()
            {
                StartDate = Convert.ToDateTime(_from),
                EndDate = Convert.ToDateTime(_to),
                User = SessionManager.User.ID != 0 ? SessionManager.User.ID : new UserManager().GetUserByEmail(SessionManager.User.Email).ID,
                Branch = new BranchManager().Branches.Where(b => b.Name == _branch).Select(b => b.ID).FirstOrDefault(),
                IsActive = true,
                CarNumber = _carNumber



            };
            var IsOrder = orderManager.Create(order);

            /*
           V ID -- V
           V User - Session V
           V StartDate - request
           V EndDate - request
           X ReturnDate --
           V CarNumber -request
           V IsActive - true
           V Branch - request
           */

            return RedirectToAction("Index");
        }
        
        public JsonResult GetAllOrders(int TZ)
        {

            List<Order> orders= orderManager.Orders.Where(o => o.User1.TZ == TZ).ToList();
            if (User.IsInRole("Employee"))
            {
                orders = orderManager.Orders.Where(o=>o.User1.TZ==TZ).ToList();
            }
            else
            {
                var user = GetUserFromSession();
            }
           
            

            return Json(OrdersVM(orders), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCurrent()
        {
            User user;
            if (!User.IsInRole("Employee"))
            {
                user = GetUserFromSession();
            }
            else
            {
                user = null;
            }
            return Json(user,JsonRequestBehavior.AllowGet);
        }

        private List<OrderVM> OrdersVM(List<Order> orders)
        {

            branchManager = new BranchManager();
            var ordersVM = new List<OrderVM>();
            foreach (var order in orders)
            {
                string returnDate = order.ReturnDate != null ? ((DateTime)order.ReturnDate).ToString("dd/MM/yy") : "תהנה מהרגע";
                var model = carManager.Models.Where(m => m.ID == order.Car.Model).FirstOrDefault();
                ordersVM.Add(
               new OrderVM()
               {
                   Branch = branchManager.Branches.Where(b => b.ID == order.Branch).FirstOrDefault().Name,
                   Car = order.Car.Serial,
                   From = ((DateTime)order.StartDate).ToString("dd/MM/yy"),
                   ID = order.ID,
                   Model = model.Name,
                   //  Price = carManager.Models.Where(m => m.ID == order.Car.Model).FirstOrDefault().Group.PerDay * (order.EndDate - order.StartDate).Days,
                   Price = model.Group.PerDay * (order.EndDate - order.StartDate).Days,
                   To = ((DateTime)order.EndDate).ToString("dd/MM/yy"),
                   Return = returnDate,
                   User = order.User1.FirstName











               });
            }
            return ordersVM;
        }

        /// <summary>
        /// Privets get null
        /// </summary>
        /// <returns></returns>
        private User GetUserFromSession()
        {
            User user = SessionManager.User.ID != 0 ? SessionManager.User : new UserManager().GetUserByEmail(SessionManager.User.Email);
            user.Roles = null;
            return user;
        }

        public JsonResult IsCarAvailable(int car,string _from,string _to)
        {
            bool b;
            try
            {
                b = orderManager.IsCarAvailable(car, Convert.ToDateTime(_from), Convert.ToDateTime(_to));
            }
            catch (Exception)
            {

                b = false;
            }
            return Json(b,JsonRequestBehavior.AllowGet);

        }

    }
}