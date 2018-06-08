using Diploma.Database;
using Diploma.EmailService;
using Diploma.Models;
using Diploma.Security;
using Diploma.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace Diploma.BLL
{
    public class AccountService : IAccountService
    {
        public User GetByKey(string userEmail)
        {
            User user = new User();
            using (DiplomaDBContext db = new DiplomaDBContext())
            {
                user = db.ListOfUsers.Where(x => x.Email == userEmail).FirstOrDefault();
                return user;
            }
        }

        public void Add(User user)
        {
            using (DiplomaDBContext db = new DiplomaDBContext())
            {
                user.Password = HashService.HashPassword(user.Password);
                db.ListOfUsers.Add(user);
                db.SaveChanges();
            }
        }

        public void Register(UserRegistrationVM user)
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

        public void ActivateAccount(string userEmail, string hash)
        {
            using (DiplomaDBContext db = new DiplomaDBContext())
            {
                User user = db.ListOfUsers.Where(x => x.Email == userEmail).FirstOrDefault();

                if (HashService.GenerateUserHash(user) == hash)
                {
                    user.EmailActivated = true;
                    db.SaveChanges();

                }
            }
        }

        public void ChangePassword(string userEmail, string newPassword)
        {
            using (DiplomaDBContext db = new DiplomaDBContext())
            {
                User user = db.ListOfUsers.Where(x => x.Email == userEmail).FirstOrDefault();

                user.Password = HashService.HashPassword(newPassword);
                db.SaveChanges();
            }
        }

    }
}
