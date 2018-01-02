using _4.Entities;
using _4.Entities.ExtentionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace _1.UI_2.Models
{
    public class ManageUsersVM
    {
        public List<User> Users { get; set; }
        public string UserRoles { get; set; }
        public List<Role> Roles { get; set; }
        public int CurrentPage { get; set; }
        public int DisplayCount { get; set; }
        public int Pages { get; set; }

        public ManageUsersVM(List<User> _users,List<Role> _roles,int _displayCount,int _currentPage=1)
        {
            Users = ClearRoles(_users);
            
            Roles = _roles;
            DisplayCount = _displayCount;
            CurrentPage = _currentPage;
            Pages = PagesCalc(Users.Count() , DisplayCount);

        }
        private int PagesCalc(int users,int count)
        {
            int pages = users / count;
            if (users % count > 0)
                pages++;
            return pages;
        }
        private List<User> ClearRoles(List<User> users)
        {
            foreach (var user in users)
            {
               user.Roles= user.Roles.Select(r => new Role() {ID=r.ID,Name=r.Name }).ToList();
                
            }



            return users;
        }
    }
}