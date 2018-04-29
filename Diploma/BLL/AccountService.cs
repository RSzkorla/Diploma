using Diploma.Database;
using Diploma.Models;
using Diploma.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diploma.BLL
{
    public static class AccountService
    {
        public static void Add(User user)
        {
            using (DiplomaDBContext db = new DiplomaDBContext())
            {
                user.Password = PasswordHash.HashPassword(user.Password);
                db.ListOfUsers.Add(user);
                db.SaveChanges();
            }
        }

        public static void ActivateAccount(string uriQuery)
        {
            var uriQuery2 = uriQuery.Replace("?userEmail=", "");
            var uriQuery3 = uriQuery2.Replace("?activationId", " ");

            var uriUserData = uriQuery3.Split();

            string userMail = uriUserData[0];
            Guid activationId = new Guid(uriUserData[1]);

            using (DiplomaDBContext db = new DiplomaDBContext())
            {
                var selectedUser = from x in db.ListOfUsers
                                   where x.Email == userMail
                                   && x.ActivationId == activationId
                                   select x;

                User activatedUser = selectedUser.FirstOrDefault();

                activatedUser.EmailActivated = true;
                db.SaveChanges();
            }
        }
    }
}