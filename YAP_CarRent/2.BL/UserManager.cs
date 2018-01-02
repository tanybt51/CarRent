using _3.DAL;
using _4.Entities;
using _4.Entities.ExtentionMethods;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.BL
{
    public class UserManager : IDisposable
    {
        private CarRentEntities ctx;

        public UserManager()
        {
            ctx = new CarRentEntities();
        }

        //C:\Users\User\Documents\ג'ון ברייס\ADO.NET\Lesson5 - EF 3 Layers\Lesson5 - EF 3 Layers\Lesson5\packages\EntityFramework.5.0.0\lib\net45\EntityFramework.dll <--Browse from the DAL Folder
        public List<Car> GetAllCars()
        {
            using (CarRentEntities ctx = new CarRentEntities())
            {
                return ctx.Cars.Include("Status").ToList();

            }
        }

        public User UserLogin(string email)
        {
            using (CarRentEntities ctx = new CarRentEntities())
            {
                return ctx.Users.Where(s => s.Email == email).FirstOrDefault();

            }

        }

        public bool RegisterNewUser(User user)
        {

            try
            {
                ctx.Users.Add(user);
                ctx.Entry(user).State = EntityState.Added;
                return ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool UpdateUser(User user)
        {
            ctx.Users.Attach(user);
            ctx.Entry(user).State = EntityState.Added;
            return ctx.SaveChanges() > 0;

        }



        public List<User> Users
        {
            get
            {
                try
                {
                    var users = ctx.Users.Include("Roles").ToList();
                    foreach (var user in users)
                    {
                        user.UserRoles = user.Roles.ToList().ToStringArray();
                        user.Password = user.Password.Trim();
                    }
                    return users;
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
        }

        public User GetUserByEmail(string _email)
        {
            return Users.Where(u => u.Email == _email).FirstOrDefault();
        }

        public List<Role> Roles
        {
            get
            {
                try
                {
                    //return ctx.Roles.Select(r => new Role() { ID = r.ID, Name = r.Name }).ToList();
                    var roles = ctx.Roles.AsNoTracking().ToList();
                    foreach (var role in roles)
                    {
                        role.Name = role.Name.Trim();
                    }
                    return roles;
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
        }

        public string[] getAllUsersNames()
        {
            try
            {
                return ctx.Users.Select(u => u.Email).ToArray();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public string[] getAllRollesNames()
        {
            try
            {
                return ctx.Roles.Select(r => r.Name).ToArray();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public string[] getAllRollesNames(string email)
        {
            try
            {
                return ctx.Roles.Select(r => r.Name).ToArray();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool UpdateLogin(User _user)
        {
            ctx.Users.Attach(_user);
            ctx.Entry(_user).State = EntityState.Modified;
            return ctx.SaveChanges() > 0;

        }

        public bool EditUser(User _user)
        {
            try
            {
                var user = ctx.Users.Attach(_user);
                ctx.Entry(user).State = EntityState.Modified;
                return ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
                //throw ex;
            }

        }

        /// <summary>
        /// Gets User and it's of Roles
        /// </summary>
        /// <param name="_user"></param>
        /// <param name="_roles"></param>
        /// <returns></returns>
        public bool AddRolesToUser1(User _user, List<Role> _roles)
        {

            try
            {
                // step 1) "caching" the DB User(from the DB!) with the Roles
                var user = ctx.Users.Include("Roles").Single(u => u.ID == _user.ID);

                // step 2) Clear all Roles(from the cache DB!)
                var roles = user.Roles.ToList();
                for (int i = 0; i < roles.Count; i++)
                {

                    var r = ctx.Roles.Attach(roles[i]);
                    ctx.Entry(roles[i]).State = EntityState.Deleted;
                    return ctx.SaveChanges() > 0;
                }

                // step 3) "caching" every single role (from the DB!) and adding it 
                //         to the user
                foreach (var _role in _roles)
                {
                    var role = ctx.Roles.Single(r => r.ID == _role.ID);
                    user.Roles.Add(role);
                }

                // step 4) Save the Changes
                return ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
                //throw ex;
            }
        }


        public bool AddRolesToUser(User _user, List<Role> _roles)
        {

            try
            {
                // step 1) "caching" the DB User(from the DB!) with the Roles
                var user = ctx.Users.Include("Roles").Single(u => u.ID == _user.ID);

                // step 2) Clear all Roles(from the cache DB!)
                user.Roles.Clear();

                // step 3) "caching" every single role (from the DB!) and adding it 
                //         to the user
                foreach (var _role in _roles)
                {
                    var role = ctx.Roles.Single(r => r.ID == _role.ID);
                    user.Roles.Add(role);
                }

                // step 4) Save the Changes
                return ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
                //throw ex;
            }
        }

        public void Dispose()
        {

            ctx.Dispose();
        }
    }

    //




}

