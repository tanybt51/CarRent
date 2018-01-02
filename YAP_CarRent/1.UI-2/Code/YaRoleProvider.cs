using _2.BL;
using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace _1.UI_2.Code
{
    public class YaRoleProvider : RoleProvider
    {
        UserManager userManager;
        private string _appName;
        public YaRoleProvider()
        {
            userManager = new UserManager();
        }
        public override string ApplicationName
        {
            get
            {
                return _appName;
            }

            set
            {
                _appName = value;
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] rolenames)
        {
            foreach (string rolename in rolenames)
            {
                if (!RoleExists(rolename))
                {
                    throw new ProviderException("Role name not found.");
                }
            }

            foreach (string username in usernames)
            {
                if (username.Contains(","))
                {
                    throw new ArgumentException("User names cannot contain commas.");
                }

                foreach (string rolename in rolenames)
                {
                    if (IsUserInRole(username, rolename))
                    {
                        throw new ProviderException("User is already in role.");
                    }
                }
            }
            // Here we can add the user a role.

        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            return userManager.getAllRollesNames();
        }

        public override string[] GetRolesForUser(string username)
        {
            using (UserManager userManager = new UserManager())
            {
                var user = userManager.Users.Where(u => u.Email == username).FirstOrDefault();

                var roles = user.Roles.Select(r => r.Name.Trim()).ToArray();

                return roles;
            }
           
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}