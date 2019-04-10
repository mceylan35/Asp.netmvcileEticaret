using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Eticaret.Entities;
using Eticaret.UnitOfWork;

namespace Eticaret.Security
{
    public class KullaniciRoleProvider:RoleProvider
    {
        EfUnitOfWork uow = new EfUnitOfWork();
        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public int GetIdForUser(string username)
        {
            var kullanici = uow.GetRepository<tbl_Kullanici>().Get(x => x.Kullanici_Adi == username);
            return kullanici.Kullanici_Id;
        }

        public override string[] GetRolesForUser(string username)
        {
            EfUnitOfWork ouw = new EfUnitOfWork();
            var kullanici = uow.GetRepository<tbl_Kullanici>().Get(x => x.Kullanici_Adi == username);
            return new string[] {kullanici.tbl_Rol.Rol};
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}