using _3.DAL;
using _4.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.BL
{
    public class BranchManager : IDisposable
    {
        private CarRentEntities ctx;
        public BranchManager()
        {
            ctx = new CarRentEntities();
        }
        //C:\Users\User\Documents\ג'ון ברייס\ADO.NET\Lesson5 - EF 3 Layers\Lesson5 - EF 3 Layers\Lesson5\packages\EntityFramework.5.0.0\lib\net45\EntityFramework.dll <--Browse from the DAL Folder
        public List<Car> GetAllCars()
        {
            return ctx.Cars.ToList();
        }

        //public List<Branch> GetAllBranches()
        //{
        //   return ctx.Branches.ToList();
        //}

        public List<Car> GetAllCarsByStatus(int statusID)
        {
            return ctx.Cars.Where(c => c.Status == statusID).ToList();
        }

        public List<Car> GetAllCarsByStatusInBranch(int statusID, int branchID)
        {
            return ctx.Cars.Where(c => c.Status == statusID && c.Branch == branchID).ToList();
        }

        public Car GetCarByID(int carID)
        {
                return ctx.Cars.Where(c => c.Serial == carID).FirstOrDefault();
        }

        public List<Branch> Branches
        {
            get
            {
                return ctx.Branches.ToList();
            }
        }

        public int BranchID(string branchName)
        {
            return Branches.Where(b => b.Name == branchName).Select(b => b.ID).FirstOrDefault();

        }

        public void Dispose()
        {
            ctx.Dispose();
        }

    }
}
