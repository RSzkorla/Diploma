using Diploma.Database;
using Diploma.EmailService;
using Diploma.Models;
using Diploma.Security;
using Diploma.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diploma.BLL
{
    public static class AccountService
    {
        public static User GetByKey(string userEmail)
        {
            User user = new User();
            using (DiplomaDBContext db = new DiplomaDBContext())
            {
                user = db.ListOfUsers.Where(x => x.Email == userEmail).FirstOrDefault();
                return user;
            }
        }

        public static void Add(User user)
        {
            using (DiplomaDBContext db = new DiplomaDBContext())
            {
                user.Password = PasswordHash.HashPassword(user.Password);
                db.ListOfUsers.Add(user);
                db.SaveChanges();
            }
        }

        public static void Register(UserRegistrationVM user)
        {
            User newUser = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                EmailActivated = false,
                Password = user.Password,
                ListOfProjects = new List<Project>()
            };

            Add(newUser);
        }

        public static string GenerateUserHash(User user)
        {
            if (user != null)
            {
                return $"{user.Email}{user.ActivationId}".GetHashCode().ToString();
            }
            else
                return null;
        }

        public static void ActivateAccount(string userEmail, string hash)
        {
            using (DiplomaDBContext db = new DiplomaDBContext())
            {
                User user = db.ListOfUsers.Where(x => x.Email == userEmail).FirstOrDefault();

                if (GenerateUserHash(user) == hash)
                {
                    user.EmailActivated = true;
                    db.SaveChanges();

                }
            }
        }

        public static void ChangePassword(string userEmail, string newPassword)
        {
            using (DiplomaDBContext db = new DiplomaDBContext())
            {
                User user = db.ListOfUsers.Where(x => x.Email == userEmail).FirstOrDefault();

                user.Password = PasswordHash.HashPassword(newPassword);
                db.SaveChanges();
            }
        }
    }

}