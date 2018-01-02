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
    public class CarManager : IDisposable
    {
        CarRentEntities ctx;

        public CarManager()
        {
            ctx = new CarRentEntities();
        }

        #region Function
        public List<Car> GetAllCars()
        {
            return ctx.Cars.Include("Branch1").Include("Status1").Include("Model1").Include("Gear1").ToList();
        }

        public List<Car> GetCarsByBranch(int branch)
        {
            return ctx.Cars.Include("Branch1").Include("Status1").Include("Model1").Include("Gear1").Where(c => c.Branch == branch).ToList();
        }

        public Car GetCarsID(int id)
        {
            return ctx.Cars.Include("Branch1").Include("Status1").Include("Model1").Include("Gear1").Where(c => c.Serial == id).FirstOrDefault();
        }

        public List<Model> GetAllModels(int manufacturer)
        {

            return ctx.Models.Where(m => m.Manufacturer == manufacturer).ToList();

        }
        #endregion

        #region Properties

        public List<Model> Models
        {
            get
            {
                return ctx.Models.Include("Group").ToList();
            }
        }

        public List<Status> Statuses
        {
            get
            {
                return ctx.Status.ToList();
            }
        }

        public List<Manufacturer> Manufacturers
        {
            get
            {
                return ctx.Manufacturers.ToList();
            }
        }

        public List<Group> Groups
        {
            get
            {
                return ctx.Groups.ToList();
            }
        }

        public List<Gear> Gears
        {
            get
            {
                return ctx.Gears.ToList();
            }
        }

        public List<Car> Cars
        {
            get
            {
                return ctx.Cars.ToList();
            }
        }

        public List<Order> Orders
        {
            get
            {
                return ctx.Orders.ToList();
            }
        }
        #endregion

        #region Creating New Car
        public bool IsSerial(int serial)
        {
            var car = ctx.Cars.Where(c => c.Serial == serial).FirstOrDefault();
            return car == null;
            // True means it's a new car

        }

        public int GearID(string name)
        {
            return Gears.Where(g => g.Name == name).Select(g => g.ID).FirstOrDefault();
        }

        public int ModelID(string name)
        {
            return Models.Where(m => m.Name == name).Select(m => m.ID).FirstOrDefault();
        }

        public int StutusID(string name)
        {
            return Statuses.Where(s => s.Name == name).Select(s => s.ID).FirstOrDefault();
        }

        #endregion

        #region Manager Function
        public bool Create(Car newCar)
        {
            try
            {

                ctx.Cars.Attach(newCar);
                ctx.Entry(newCar).State = EntityState.Added;

                return ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool Edit(Car _car)
        {
            try
            {

                ctx.Cars.Attach(_car);
                ctx.Entry(_car).State = EntityState.Modified;

                return ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool Delete(int id)
        {
            try
            {
                var _car = ctx.Cars.Where(c => c.Serial == id).FirstOrDefault();
                ctx.Cars.Attach(_car);
                ctx.Entry(_car).State = EntityState.Deleted;

                return ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion


        public void Dispose()
        {
            ctx.Dispose();
        }
    }
}

//C:\Users\User\Documents\ג'ון ברייס\ADO.NET\Lesson5 - EF 3 Layers\Lesson5 - EF 3 Layers\Lesson5\packages\EntityFramework.5.0.0\lib\net45\EntityFramework.dll <--Browse from the DAL Folder
