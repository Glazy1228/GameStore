using System;
using System.Linq;
using System.Web.Security;
using GameStore.WEB.Models;
using GameStore.BLL.Interfaces;
using System.Data.Entity;

namespace GameStore.WEB.Providers
{
    public class CustomRoleProvider : RoleProvider
    {

        public override string[] GetRolesForUser(string username)
        {
            if (username.Equals("Admin"))
            {
                return new[] { "Admin"};
            }
            if (username.EndsWith("Moderator"))
            {
                return new[] { "Moderator" };
            }
            return new string[] { "User" };
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
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
            return new[] { "Admin", "User", "Moderator" };
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