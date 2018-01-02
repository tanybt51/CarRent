using _3.DAL;
using _4.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.BL
{
    public class OrderManager : IDisposable
    {
        CarRentEntities ctx;

        public OrderManager()
        {
            ctx = new CarRentEntities();
        }

        #region Function
        public List<Order> GetAllOrders()
        {
            return ctx.Orders.ToList();
        }

        public List<Order> GetOrderByBranch(int _branch)
        {
            return ctx.Orders.Where(o => o.Branch1.ID == _branch).ToList();
        }

        public List<Order> GetOrderByUser(int _user)
        {

            return ctx.Orders.Where(o => o.User == _user).ToList();

        }

        public bool IsCarAvailable(int _car, DateTime _from, DateTime _to)
        {
            List<Order> orders = new List<Order>();
            //   var orders = ctx.Orders.Where(o => o.CarNumber == _car).Select(o => o.StartDate <= _from && o.EndDate >= _from || o.StartDate <= _to && o.EndDate >= _to).ToList();
            var _orders = ctx.Orders.Where(o => o.CarNumber == _car).ToList();
            //  var orders = _orders.Select(o => o.StartDate <= _from && o.EndDate >= _from || o.StartDate <= _to && o.EndDate >= _to).ToList();
            foreach (var order in _orders)
            {
                if(order.StartDate<=_from && order.EndDate>=_from|| order.StartDate <= _to && order.EndDate >= _to)
                {
                    orders.Add(order);
                }
            } 



            return orders.Count()==0;


        }

        public List<Car> GetAvailableCars(List<Car> _cars,  DateTime _from, DateTime _to)
        {

            var cars = _cars.Where(c => IsCarAvailable(c.Serial, _from, _to)).ToList();
            return cars;


        }

        public bool Create(Order order)
        {
            try
            {

                ctx.Orders.Attach(order);
                ctx.Entry(order).State = EntityState.Added;

                return ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {

                return false;
            }

        }

        public bool CloseOreder(Order _order)
        {
            try
            {
                ctx.Orders.Attach(_order);
                ctx.Entry(_order).State = EntityState.Modified;
                return ctx.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion

        #region Properties


        public List<Order> Orders
        {
            get
            {
                return ctx.Orders.Include("Car").Include("User1").ToList();
            }
        }
        #endregion

     

        #region Manager Function
        public List<User> GetAllUser()
        {
           return ctx.Orders.Include("User").Select(o => o.User1).Distinct().ToList();
        }



        #endregion


        public void Dispose()
        {
            ctx.Dispose();
        }
    }
}

//C:\Users\User\Documents\ג'ון ברייס\ADO.NET\Lesson5 - EF 3 Layers\Lesson5 - EF 3 Layers\Lesson5\packages\EntityFramework.5.0.0\lib\net45\EntityFramework.dll <--Browse from the DAL Folder
