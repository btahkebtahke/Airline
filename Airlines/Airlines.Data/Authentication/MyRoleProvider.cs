using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Providers.Entities;
using System.Web.Security;
using System.Data.Entity;
using Airlines.Data.Repository;

namespace Airlines.Data.Authentication
{
    //Overrided methods for universal providers to use them for Authentication
    public class MyRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string username)
        {
            string[] roles = new string[] { };
            using (AirlineContext db = new AirlineContext())
            {
                // Getting user
                AuthEntities.User user = db.Users.Include(u => u.Role).FirstOrDefault(u => u.Username == username);
                if (user != null && user.Role != null)
                {
                    // Getting role
                    roles = new string[] { user.Role.Name };
                }
                return roles;
            }
        }
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            using (AirlineContext db = new AirlineContext())
            {
                // Getting user

                AuthEntities.User user = db.Users.Include(u => u.Role).FirstOrDefault(u => u.Username == username);

                if (user != null && user.Role != null && user.Role.Name == roleName)
                    return true;
                else
                    return false;
            }
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
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
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
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
